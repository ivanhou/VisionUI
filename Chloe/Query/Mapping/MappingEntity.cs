using Chloe.Mapper;
using Chloe.Descriptors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Chloe.Query.Mapping
{
    public class MappingEntity : IObjectActivatorCreator
    {
        public MappingEntity(EntityConstructorDescriptor constructorDescriptor)
        {
            this.ConstructorDescriptor = constructorDescriptor;
            this.ConstructorParameters = new Dictionary<ParameterInfo, int>();
            this.ConstructorEntityParameters = new Dictionary<ParameterInfo, IObjectActivatorCreator>();
            this.Members = new Dictionary<MemberInfo, int>();
            this.EntityMembers = new Dictionary<MemberInfo, IObjectActivatorCreator>();
        }
        public int? CheckNullOrdinal { get; set; }
        public EntityConstructorDescriptor ConstructorDescriptor { get; private set; }
        public Dictionary<ParameterInfo, int> ConstructorParameters { get; private set; }
        public Dictionary<ParameterInfo, IObjectActivatorCreator> ConstructorEntityParameters { get; private set; }

        public Dictionary<MemberInfo, int> Members { get; private set; }
        public Dictionary<MemberInfo, IObjectActivatorCreator> EntityMembers { get; private set; }

        public IObjectActivator CreateObjectActivator()
        {
            return this.CreateObjectActivator(null);
        }
        public IObjectActivator CreateObjectActivator(IDbContext dbContext)
        {
            EntityMemberMapper mapper = this.ConstructorDescriptor.GetEntityMemberMapper();
            List<IValueSetter> memberSetters = new List<IValueSetter>(this.Members.Count + this.EntityMembers.Count);
            foreach (var kv in this.Members)
            {
                IMRM mMapper = mapper.TryGetMemberMapper(kv.Key);
                MappingMemberBinder binder = new MappingMemberBinder(mMapper, kv.Value);
                memberSetters.Add(binder);
            }

            foreach (var kv in this.EntityMembers)
            {
                Action<object, object> del = mapper.TryGetNavigationMemberSetter(kv.Key);
                IObjectActivator memberActivtor = kv.Value.CreateObjectActivator();
                NavigationMemberBinder binder = new NavigationMemberBinder(del, memberActivtor);
                memberSetters.Add(binder);
            }

            Func<IDataReader, ReaderOrdinalEnumerator, ObjectActivatorEnumerator, object> instanceCreator = this.ConstructorDescriptor.GetInstanceCreator();

            List<int> readerOrdinals = this.ConstructorParameters.Select(a => a.Value).ToList();
            List<IObjectActivator> objectActivators = this.ConstructorEntityParameters.Select(a => a.Value.CreateObjectActivator()).ToList();

            ObjectActivator ret;
            if (dbContext != null)
                ret = new ObjectActivatorWithTracking(instanceCreator, readerOrdinals, objectActivators, memberSetters, this.CheckNullOrdinal, dbContext);
            else
                ret = new ObjectActivator(instanceCreator, readerOrdinals, objectActivators, memberSetters, this.CheckNullOrdinal);

            return ret;
        }
    }
}
