using Chloe.Core.Visitors;
using Chloe.DbExpressions;
using Chloe.Entity;
using Chloe.Exceptions;
using Chloe.Infrastructure;
using Chloe.InternalExtensions;
using Chloe.Query.Visitors;
using Chloe.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Chloe.Descriptors
{
    public class TypeDescriptor
    {
        Dictionary<MemberInfo, MappingMemberDescriptor> _mappingMemberDescriptors;
        Dictionary<MemberInfo, DbColumnAccessExpression> _memberColumnMap;
        MappingMemberDescriptor _primaryKey = null;
        MappingMemberDescriptor _autoIncrement = null;

        DefaultExpressionVisitor _visitor = null;

        TypeDescriptor(Type t)
        {
            this.EntityType = t;
            this.Init();
        }

        void Init()
        {
            this.InitTableInfo();
            this.InitMemberInfo();
            this.InitMemberColumnMap();
        }
        void InitTableInfo()
        {
            Type t = this.EntityType;
            var tableFlags = t.GetCustomAttributes(typeof(TableAttribute), false);

            string tableName;
            if (tableFlags.Length > 0)
            {
                TableAttribute tableFlag = (TableAttribute)tableFlags.First();
                if (tableFlag.Name != null)
                    tableName = tableFlag.Name;
                else
                    tableName = t.Name;
            }
            else
                tableName = t.Name;

            this.Table = new DbTable(tableName);
        }
        void InitMemberInfo()
        {
            List<MappingMemberDescriptor> mappingMemberDescriptors = this.ExtractMappingMemberDescriptors();

            int primaryKeyCount = mappingMemberDescriptors.Where(a => a.IsPrimaryKey).Count();
            if (primaryKeyCount > 1)
                throw new NotSupportedException(string.Format("The entity type '{0}' can't define multiple primary keys.", this.EntityType.FullName));
            else if (primaryKeyCount == 1)
            {
                this._primaryKey = mappingMemberDescriptors.Where(a => a.IsPrimaryKey).First();
            }
            else
            {
                //如果没有定义任何主键，则从所有映射的属性中查找名为 id 的属性作为主键
                MappingMemberDescriptor idNameMemberDescriptor = mappingMemberDescriptors.Where(a => a.MemberInfo.Name.ToLower() == "id" && !a.IsDefined(typeof(ColumnAttribute))).FirstOrDefault();

                if (idNameMemberDescriptor != null)
                {
                    idNameMemberDescriptor.IsPrimaryKey = true;
                    this._primaryKey = idNameMemberDescriptor;
                }
            }

            List<MappingMemberDescriptor> autoIncrementMemberDescriptors = mappingMemberDescriptors.Where(a => a.IsDefined(typeof(AutoIncrementAttribute))).ToList();
            if (autoIncrementMemberDescriptors.Count > 1)
            {
                throw new NotSupportedException(string.Format("The entity type '{0}' can not define multiple autoIncrement members.", this.EntityType.FullName));
            }
            else if (autoIncrementMemberDescriptors.Count == 1)
            {
                MappingMemberDescriptor autoIncrementMemberDescriptor = autoIncrementMemberDescriptors[0];
                if (autoIncrementMemberDescriptor.IsDefined(typeof(NonAutoIncrementAttribute)))
                {
                    throw new ChloeException(string.Format("Can't define both 'AutoIncrementAttribute' and 'NotAutoIncrementAttribute' on the same mapping member '{0}'.", autoIncrementMemberDescriptor.MemberInfo.Name));
                }

                if (!IsAutoIncrementType(autoIncrementMemberDescriptor.MemberInfoType))
                {
                    throw new ChloeException("AutoIncrement member type must be Int16,Int32 or Int64.");
                }

                autoIncrementMemberDescriptor.IsAutoIncrement = true;
                this._autoIncrement = autoIncrementMemberDescriptor;
            }
            else
            {
                MappingMemberDescriptor defaultAutoIncrementMemberDescriptor = mappingMemberDescriptors.Where(a => a.IsPrimaryKey && IsAutoIncrementType(a.MemberInfoType) && !a.IsDefined(typeof(NonAutoIncrementAttribute))).FirstOrDefault();
                if (defaultAutoIncrementMemberDescriptor != null)
                {
                    defaultAutoIncrementMemberDescriptor.IsAutoIncrement = true;
                    this._autoIncrement = defaultAutoIncrementMemberDescriptor;
                }
            }

            this._mappingMemberDescriptors = new Dictionary<MemberInfo, MappingMemberDescriptor>(mappingMemberDescriptors.Count);
            foreach (MappingMemberDescriptor mappingMemberDescriptor in mappingMemberDescriptors)
            {
                this._mappingMemberDescriptors.Add(mappingMemberDescriptor.MemberInfo, mappingMemberDescriptor);
            }
        }
        void InitMemberColumnMap()
        {
            Dictionary<MemberInfo, DbColumnAccessExpression> memberColumnMap = new Dictionary<MemberInfo, DbColumnAccessExpression>(this._mappingMemberDescriptors.Count);
            foreach (var kv in this._mappingMemberDescriptors)
            {
                memberColumnMap.Add(kv.Key, new DbColumnAccessExpression(this.Table, kv.Value.Column));
            }

            this._memberColumnMap = memberColumnMap;
        }

        List<MappingMemberDescriptor> ExtractMappingMemberDescriptors()
        {
            var members = this.EntityType.GetMembers(BindingFlags.Public | BindingFlags.Instance);

            List<MappingMemberDescriptor> mappingMemberDescriptors = new List<MappingMemberDescriptor>();
            foreach (var member in members)
            {
                if (ShouldMap(member) == false)
                    continue;

                if (MappingTypeSystem.IsMappingType(member.GetMemberType()))
                {
                    MappingMemberDescriptor memberDescriptor = new MappingMemberDescriptor(member, this);
                    mappingMemberDescriptors.Add(memberDescriptor);
                }
            }

            return mappingMemberDescriptors;
        }

        static bool IsAutoIncrementType(Type t)
        {
            return t == UtilConstants.TypeOfInt16 || t == UtilConstants.TypeOfInt32 || t == UtilConstants.TypeOfInt64;
        }
        static bool ShouldMap(MemberInfo member)
        {
            var ignoreFlags = member.GetCustomAttributes(typeof(NotMappedAttribute), false);
            if (ignoreFlags.Length > 0)
                return false;

            if (member.MemberType == MemberTypes.Property)
            {
                if (((PropertyInfo)member).GetSetMethod() == null)
                    return false;//对于没有公共的 setter 直接跳过
                return true;
            }
            else if (member.MemberType == MemberTypes.Field)
            {
                return true;
            }
            else
                return false;//只支持公共属性和字段
        }

        public Type EntityType { get; private set; }
        public DbTable Table { get; private set; }

        public MappingMemberDescriptor PrimaryKey { get { return this._primaryKey; } }
        public MappingMemberDescriptor AutoIncrement { get { return this._autoIncrement; } }
        public DefaultExpressionVisitor Visitor
        {
            get
            {
                if (this._visitor == null)
                    this._visitor = new DefaultExpressionVisitor(this);

                return this._visitor;
            }
        }

        public Dictionary<MemberInfo, MappingMemberDescriptor> MappingMemberDescriptors { get { return this._mappingMemberDescriptors; } }

        public bool HasPrimaryKey()
        {
            return this._primaryKey != null;
        }
        public MappingMemberDescriptor TryGetMappingMemberDescriptor(MemberInfo memberInfo)
        {
            memberInfo = memberInfo.AsReflectedMemberOf(this.EntityType);
            MappingMemberDescriptor memberDescriptor;
            this._mappingMemberDescriptors.TryGetValue(memberInfo, out memberDescriptor);
            return memberDescriptor;
        }
        public DbColumnAccessExpression TryGetColumnAccessExpression(MemberInfo memberInfo)
        {
            memberInfo = memberInfo.AsReflectedMemberOf(this.EntityType);
            DbColumnAccessExpression dbColumnAccessExpression;
            this._memberColumnMap.TryGetValue(memberInfo, out dbColumnAccessExpression);
            return dbColumnAccessExpression;
        }

        static readonly System.Collections.Concurrent.ConcurrentDictionary<Type, TypeDescriptor> InstanceCache = new System.Collections.Concurrent.ConcurrentDictionary<Type, TypeDescriptor>();

        public static TypeDescriptor GetDescriptor(Type type)
        {
            TypeDescriptor instance;
            if (!InstanceCache.TryGetValue(type, out instance))
            {
                lock (type)
                {
                    if (!InstanceCache.TryGetValue(type, out instance))
                    {
                        instance = new TypeDescriptor(type);
                        InstanceCache.GetOrAdd(type, instance);
                    }
                }
            }

            return instance;
        }
    }
}
