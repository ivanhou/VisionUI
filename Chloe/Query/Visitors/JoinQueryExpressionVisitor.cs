using Chloe.DbExpressions;
using Chloe.Descriptors;
using Chloe.Query.QueryExpressions;
using Chloe.Query.QueryState;
using Chloe.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Chloe.Query.Visitors
{
    class JoinQueryExpressionVisitor : QueryExpressionVisitor<JoinQueryResult>
    {
        ResultElement _resultElement;
        DbJoinType _joinType;

        LambdaExpression _conditionExpression;
        List<IMappingObjectExpression> _moeList;

        JoinQueryExpressionVisitor(ResultElement resultElement, DbJoinType joinType, LambdaExpression conditionExpression, List<IMappingObjectExpression> moeList)
        {
            this._resultElement = resultElement;
            this._joinType = joinType;
            this._conditionExpression = conditionExpression;
            this._moeList = moeList;
        }

        public static JoinQueryResult VisitQueryExpression(QueryExpression queryExpression, ResultElement resultElement, DbJoinType joinType, LambdaExpression conditionExpression, List<IMappingObjectExpression> moeList)
        {
            JoinQueryExpressionVisitor visitor = new JoinQueryExpressionVisitor(resultElement, joinType, conditionExpression, moeList);
            return queryExpression.Accept(visitor);
        }

        public override JoinQueryResult Visit(RootQueryExpression exp)
        {
            Type type = exp.ElementType;
            TypeDescriptor typeDescriptor = TypeDescriptor.GetDescriptor(type);

            string alias = this._resultElement.GenerateUniqueTableAlias(typeDescriptor.Table.Name);

            DbTableSegment tableSeg = CreateTableExpression(typeDescriptor.Table, alias);
            MappingObjectExpression moe = new MappingObjectExpression(typeDescriptor.EntityType.GetConstructor(Type.EmptyTypes));

            DbTable table = new DbTable(alias);
            foreach (MappingMemberDescriptor item in typeDescriptor.MappingMemberDescriptors.Values)
            {
                DbColumnAccessExpression columnAccessExpression = new DbColumnAccessExpression(table, item.Column);
                moe.AddMemberExpression(item.MemberInfo, columnAccessExpression);

                if (item.IsPrimaryKey)
                    moe.PrimaryKey = columnAccessExpression;
            }

            //TODO 解析 on 条件表达式
            DbExpression condition = null;
            List<IMappingObjectExpression> moeList = new List<IMappingObjectExpression>(this._moeList.Count + 1);
            moeList.AddRange(this._moeList);
            moeList.Add(moe);
            condition = GeneralExpressionVisitor.ParseLambda(this._conditionExpression, moeList);

            DbJoinTableExpression joinTable = new DbJoinTableExpression(this._joinType, tableSeg, condition);

            JoinQueryResult result = new JoinQueryResult();
            result.MappingObjectExpression = moe;
            result.JoinTable = joinTable;

            return result;
        }
        public override JoinQueryResult Visit(WhereExpression exp)
        {
            JoinQueryResult ret = this.Visit(exp);
            return ret;
        }
        public override JoinQueryResult Visit(OrderExpression exp)
        {
            JoinQueryResult ret = this.Visit(exp);
            return ret;
        }
        public override JoinQueryResult Visit(SelectExpression exp)
        {
            JoinQueryResult ret = this.Visit(exp);
            return ret;
        }
        public override JoinQueryResult Visit(SkipExpression exp)
        {
            JoinQueryResult ret = this.Visit(exp);
            return ret;
        }
        public override JoinQueryResult Visit(TakeExpression exp)
        {
            JoinQueryResult ret = this.Visit(exp);
            return ret;
        }
        public override JoinQueryResult Visit(AggregateQueryExpression exp)
        {
            JoinQueryResult ret = this.Visit(exp);
            return ret;
        }
        public override JoinQueryResult Visit(JoinQueryExpression exp)
        {
            JoinQueryResult ret = this.Visit(exp);
            return ret;
        }
        public override JoinQueryResult Visit(GroupingQueryExpression exp)
        {
            JoinQueryResult ret = this.Visit(exp);
            return ret;
        }

        JoinQueryResult Visit(QueryExpression exp)
        {
            IQueryState state = QueryExpressionVisitor.VisitQueryExpression(exp);
            JoinQueryResult ret = state.ToJoinQueryResult(this._joinType, this._conditionExpression, this._resultElement.FromTable, this._moeList, this._resultElement.GenerateUniqueTableAlias());
            return ret;
        }
        static DbTableSegment CreateTableExpression(DbTable table, string alias)
        {
            DbTableExpression tableExp = new DbTableExpression(table);
            DbTableSegment tableSeg = new DbTableSegment(tableExp, alias);
            return tableSeg;
        }
    }
}
