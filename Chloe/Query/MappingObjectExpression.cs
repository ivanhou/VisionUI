using Chloe.Extensions;
using Chloe.InternalExtensions;
using Chloe.DbExpressions;
using Chloe.Descriptors;
using Chloe.Query.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Chloe.Utility;

namespace Chloe.Query
{
    public class MappingObjectExpression : IMappingObjectExpression
    {
        public MappingObjectExpression(ConstructorInfo constructor)
            : this(EntityConstructorDescriptor.GetInstance(constructor))
        {
        }
        public MappingObjectExpression(EntityConstructorDescriptor constructorDescriptor)
        {
            this.ConstructorDescriptor = constructorDescriptor;
            this.ConstructorParameters = new Dictionary<ParameterInfo, DbExpression>();
            this.ConstructorEntityParameters = new Dictionary<ParameterInfo, IMappingObjectExpression>();
            this.SelectedMembers = new Dictionary<MemberInfo, DbExpression>();
            this.SubResultEntities = new Dictionary<MemberInfo, IMappingObjectExpression>();
        }

        public DbExpression PrimaryKey { get; set; }
        public DbExpression NullChecking { get; set; }

        /// <summary>
        /// 返回类型
        /// </summary>
        public EntityConstructorDescriptor ConstructorDescriptor { get; private set; }
        public Dictionary<ParameterInfo, DbExpression> ConstructorParameters { get; private set; }
        public Dictionary<ParameterInfo, IMappingObjectExpression> ConstructorEntityParameters { get; private set; }
        public Dictionary<MemberInfo, DbExpression> SelectedMembers { get; protected set; }
        public Dictionary<MemberInfo, IMappingObjectExpression> SubResultEntities { get; protected set; }

        public void AddConstructorParameter(ParameterInfo p, DbExpression exp)
        {
            this.ConstructorParameters.Add(p, exp);
        }
        public void AddConstructorEntityParameter(ParameterInfo p, IMappingObjectExpression exp)
        {
            this.ConstructorEntityParameters.Add(p, exp);
        }
        public void AddMemberExpression(MemberInfo memberInfo, DbExpression exp)
        {
            memberInfo = memberInfo.AsReflectedMemberOf(this.ConstructorDescriptor.ConstructorInfo.DeclaringType);
            this.SelectedMembers.Add(memberInfo, exp);
        }
        public void AddNavMemberExpression(MemberInfo memberInfo, IMappingObjectExpression moe)
        {
            memberInfo = memberInfo.AsReflectedMemberOf(this.ConstructorDescriptor.ConstructorInfo.DeclaringType);
            this.SubResultEntities.Add(memberInfo, moe);
        }
        /// <summary>
        /// 考虑匿名函数构造函数参数和其只读属性的对应
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public DbExpression GetMemberExpression(MemberInfo memberInfo)
        {
            memberInfo = memberInfo.AsReflectedMemberOf(this.ConstructorDescriptor.ConstructorInfo.DeclaringType);
            DbExpression ret = null;
            if (!this.SelectedMembers.TryGetValue(memberInfo, out ret))
            {
                ParameterInfo p = null;
                if (!this.ConstructorDescriptor.MemberParameterMap.TryGetValue(memberInfo, out p))
                {
                    return null;
                }

                if (!this.ConstructorParameters.TryGetValue(p, out ret))
                {
                    return null;
                }
            }

            return ret;
        }
        public IMappingObjectExpression GetNavMemberExpression(MemberInfo memberInfo)
        {
            memberInfo = memberInfo.AsReflectedMemberOf(this.ConstructorDescriptor.ConstructorInfo.DeclaringType);
            IMappingObjectExpression ret = null;
            if (!this.SubResultEntities.TryGetValue(memberInfo, out ret))
            {
                //从构造函数中查
                ParameterInfo p = null;
                if (!this.ConstructorDescriptor.MemberParameterMap.TryGetValue(memberInfo, out p))
                {
                    return null;
                }

                if (!this.ConstructorEntityParameters.TryGetValue(p, out ret))
                {
                    return null;
                }
            }

            return ret;
        }
        public DbExpression GetDbExpression(MemberExpression memberExpressionDeriveFromParameter)
        {
            Stack<MemberExpression> memberExpressions = ExpressionExtension.Reverse(memberExpressionDeriveFromParameter);

            DbExpression ret = null;
            IMappingObjectExpression moe = this;
            foreach (MemberExpression memberExpression in memberExpressions)
            {
                MemberInfo accessedMember = memberExpression.Member;

                if (moe == null && ret != null)
                {
                    /* a.F_DateTime.Value.Date */
                    ret = DbExpression.MemberAccess(accessedMember, ret);
                    continue;
                }

                /* **.accessedMember */
                DbExpression e = moe.GetMemberExpression(accessedMember);
                if (e == null)
                {
                    /* Indicate current accessed member is not mapping member,then try get complex member like 'a.Order' */
                    moe = moe.GetNavMemberExpression(accessedMember);

                    if (moe == null)
                    {
                        if (ret == null)
                        {
                            /*
                             * If run here,the member access expression must be like 'a.xx',
                             * and member 'xx' is neither mapping member nor complex member,in this case,we not supported.
                             */
                            throw new InvalidOperationException(memberExpressionDeriveFromParameter.ToString());
                        }
                        else
                        {
                            /* Non mapping member is not found also,then convert linq MemberExpression to DbMemberExpression */
                            ret = DbExpression.MemberAccess(accessedMember, ret);
                            continue;
                        }
                    }

                    if (ret != null)
                    {
                        /* This case and case #110 will not appear in normal,if you meet,please email me(so_while@163.com) or call 911 for help. */
                        throw new InvalidOperationException(memberExpressionDeriveFromParameter.ToString());
                    }
                }
                else
                {
                    if (ret != null)//Case: #110
                        throw new InvalidOperationException(memberExpressionDeriveFromParameter.ToString());

                    ret = e;
                }
            }

            if (ret == null)
            {
                /*
                 * If run here,the input argument 'memberExpressionDeriveFromParameter' expression must be like 'a.xx','a.**.xx','a.**.**.xx' ...and so on,
                 * and the last accessed member 'xx' is not mapping member,in this case,we not supported too.
                 */
                throw new InvalidOperationException(memberExpressionDeriveFromParameter.ToString());
            }

            return ret;
        }
        public IMappingObjectExpression GetNavMemberExpression(MemberExpression memberExpressionDeriveParameter)
        {
            Stack<MemberExpression> memberExpressions = ExpressionExtension.Reverse(memberExpressionDeriveParameter);

            if (memberExpressions.Count == 0)
                throw new Exception();

            IMappingObjectExpression ret = this;
            foreach (MemberExpression memberExpression in memberExpressions)
            {
                MemberInfo member = memberExpression.Member;

                ret = ret.GetNavMemberExpression(member);
                if (ret == null)
                {
                    throw new NotSupportedException(memberExpressionDeriveParameter.ToString());
                }
            }

            return ret;
        }
        public IObjectActivatorCreator GenarateObjectActivatorCreator(DbSqlQueryExpression sqlQuery)
        {
            MappingEntity mappingEntity = new MappingEntity(this.ConstructorDescriptor);
            MappingObjectExpression mappingMembers = this;

            foreach (var kv in this.ConstructorParameters)
            {
                ParameterInfo pi = kv.Key;
                DbExpression exp = kv.Value;

                int ordinal;
                ordinal = MappingObjectExpressionHelper.TryGetOrAddColumn(sqlQuery, exp, pi.Name).Value;

                if (exp == this.NullChecking)
                    mappingEntity.CheckNullOrdinal = ordinal;

                mappingEntity.ConstructorParameters.Add(pi, ordinal);
            }

            foreach (var kv in mappingMembers.ConstructorEntityParameters)
            {
                ParameterInfo pi = kv.Key;
                IMappingObjectExpression val = kv.Value;

                IObjectActivatorCreator navMappingMember = val.GenarateObjectActivatorCreator(sqlQuery);
                mappingEntity.ConstructorEntityParameters.Add(pi, navMappingMember);
            }

            foreach (var kv in mappingMembers.SelectedMembers)
            {
                MemberInfo member = kv.Key;
                DbExpression exp = kv.Value;

                int ordinal;
                ordinal = MappingObjectExpressionHelper.TryGetOrAddColumn(sqlQuery, exp, member.Name).Value;

                if (exp == this.NullChecking)
                    mappingEntity.CheckNullOrdinal = ordinal;

                mappingEntity.Members.Add(member, ordinal);
            }

            foreach (var kv in mappingMembers.SubResultEntities)
            {
                MemberInfo member = kv.Key;
                IMappingObjectExpression val = kv.Value;

                IObjectActivatorCreator navMappingMember = val.GenarateObjectActivatorCreator(sqlQuery);
                mappingEntity.EntityMembers.Add(kv.Key, navMappingMember);
            }

            if (mappingEntity.CheckNullOrdinal == null)
                mappingEntity.CheckNullOrdinal = MappingObjectExpressionHelper.TryGetOrAddColumn(sqlQuery, this.NullChecking);

            return mappingEntity;
        }

        public IMappingObjectExpression ToNewObjectExpression(DbSqlQueryExpression sqlQuery, DbTable table)
        {
            MappingObjectExpression moe = new MappingObjectExpression(this.ConstructorDescriptor);
            MappingObjectExpression mappingMembers = this;

            foreach (var kv in this.ConstructorParameters)
            {
                ParameterInfo pi = kv.Key;
                DbExpression exp = kv.Value;

                DbColumnAccessExpression cae = null;
                cae = MappingObjectExpressionHelper.ParseColumnAccessExpression(sqlQuery, table, exp, pi.Name);

                moe.AddConstructorParameter(pi, cae);
            }

            foreach (var kv in mappingMembers.ConstructorEntityParameters)
            {
                ParameterInfo pi = kv.Key;
                IMappingObjectExpression val = kv.Value;

                IMappingObjectExpression navMappingMember = val.ToNewObjectExpression(sqlQuery, table);
                moe.AddConstructorEntityParameter(pi, navMappingMember);
            }

            foreach (var kv in mappingMembers.SelectedMembers)
            {
                MemberInfo member = kv.Key;
                DbExpression exp = kv.Value;

                DbColumnAccessExpression cae = null;
                cae = MappingObjectExpressionHelper.ParseColumnAccessExpression(sqlQuery, table, exp, member.Name);

                moe.AddMemberExpression(member, cae);

                if (exp == this.PrimaryKey)
                {
                    moe.PrimaryKey = cae;
                    if (this.NullChecking == this.PrimaryKey)
                        moe.NullChecking = cae;
                }
            }

            foreach (var kv in mappingMembers.SubResultEntities)
            {
                MemberInfo member = kv.Key;
                IMappingObjectExpression val = kv.Value;

                IMappingObjectExpression navMappingMember = val.ToNewObjectExpression(sqlQuery, table);
                moe.AddNavMemberExpression(member, navMappingMember);
            }

            if (moe.NullChecking == null)
                moe.NullChecking = MappingObjectExpressionHelper.TryGetOrAddNullChecking(sqlQuery, table, this.NullChecking);

            return moe;
        }

        public void SetNullChecking(DbExpression exp)
        {
            if (this.NullChecking == null)
            {
                if (this.PrimaryKey != null)
                    this.NullChecking = this.PrimaryKey;
                else
                    this.NullChecking = exp;
            }

            foreach (var item in this.ConstructorEntityParameters.Values)
            {
                item.SetNullChecking(exp);
            }

            foreach (var item in this.SubResultEntities.Values)
            {
                item.SetNullChecking(exp);
            }
        }
    }
}
