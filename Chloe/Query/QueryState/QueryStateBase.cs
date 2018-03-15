using Chloe.DbExpressions;
using Chloe.Query.Mapping;
using Chloe.Query.QueryExpressions;
using Chloe.Query.Visitors;
using Chloe.Utility;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Chloe.Core.Visitors;

namespace Chloe.Query.QueryState
{
    abstract class QueryStateBase : IQueryState
    {
        ResultElement _resultElement;
        List<IMappingObjectExpression> _moeList = null;
        protected QueryStateBase(ResultElement resultElement)
        {
            this._resultElement = resultElement;
        }

        protected List<IMappingObjectExpression> MoeList
        {
            get
            {
                if (this._moeList == null)
                    this._moeList = new List<IMappingObjectExpression>(1) { this._resultElement.MappingObjectExpression };

                return this._moeList;
            }
        }

        public virtual ResultElement Result
        {
            get
            {
                return this._resultElement;
            }
        }

        public virtual IQueryState Accept(WhereExpression exp)
        {
            DbExpression whereCondition = FilterPredicateExpressionVisitor.ParseFilterPredicate(exp.Predicate, this.MoeList);
            this._resultElement.AppendCondition(whereCondition);

            return this;
        }
        public virtual IQueryState Accept(OrderExpression exp)
        {
            if (exp.NodeType == QueryExpressionType.OrderBy || exp.NodeType == QueryExpressionType.OrderByDesc)
                this._resultElement.Orderings.Clear();

            DbOrdering ordering = VisistOrderExpression(this.MoeList, exp);

            if (this._resultElement.InheritOrderings)
            {
                this._resultElement.Orderings.Clear();
                this._resultElement.InheritOrderings = false;
            }

            this._resultElement.Orderings.Add(ordering);

            return this;
        }
        public virtual IQueryState Accept(SelectExpression exp)
        {
            ResultElement result = this.CreateNewResult(exp.Selector);
            return this.CreateQueryState(result);
        }
        public virtual IQueryState Accept(SkipExpression exp)
        {
            SkipQueryState state = new SkipQueryState(this.Result, exp.Count);
            return state;
        }
        public virtual IQueryState Accept(TakeExpression exp)
        {
            TakeQueryState state = new TakeQueryState(this.Result, exp.Count);
            return state;
        }
        public virtual IQueryState Accept(AggregateQueryExpression exp)
        {
            List<DbExpression> dbArguments = new List<DbExpression>(exp.Arguments.Count);
            foreach (Expression argument in exp.Arguments)
            {
                var dbArgument = GeneralExpressionVisitor.ParseLambda((LambdaExpression)argument, this.MoeList);
                dbArguments.Add(dbArgument);
            }

            DbAggregateExpression dbAggregateExp = new DbAggregateExpression(exp.ElementType, exp.Method, dbArguments);
            MappingFieldExpression mfe = new MappingFieldExpression(exp.ElementType, dbAggregateExp);

            ResultElement result = new ResultElement();

            result.MappingObjectExpression = mfe;
            result.FromTable = this._resultElement.FromTable;
            result.AppendCondition(this._resultElement.Condition);

            AggregateQueryState state = new AggregateQueryState(result);
            return state;
        }
        public virtual IQueryState Accept(GroupingQueryExpression exp)
        {
            List<IMappingObjectExpression> moeList = this.MoeList;
            foreach (LambdaExpression item in exp.GroupKeySelectors)
            {
                var dbExp = GeneralExpressionVisitor.ParseLambda(item, moeList);
                this._resultElement.GroupSegments.Add(dbExp);
            }

            foreach (LambdaExpression havingPredicate in exp.HavingPredicates)
            {
                var havingCondition = FilterPredicateExpressionVisitor.ParseFilterPredicate(havingPredicate, moeList);
                this._resultElement.AppendHavingCondition(havingCondition);
            }

            var newResult = this.CreateNewResult(exp.Selector);
            return new GroupingQueryState(newResult);
        }

        public virtual ResultElement CreateNewResult(LambdaExpression selector)
        {
            ResultElement result = new ResultElement();
            result.FromTable = this._resultElement.FromTable;

            IMappingObjectExpression r = SelectorExpressionVisitor.VisitSelectExpression(selector, this.MoeList);
            result.MappingObjectExpression = r;
            result.Orderings.AddRange(this._resultElement.Orderings);
            result.AppendCondition(this._resultElement.Condition);

            result.GroupSegments.AddRange(this._resultElement.GroupSegments);
            result.AppendHavingCondition(this._resultElement.HavingCondition);

            return result;
        }
        public virtual IQueryState CreateQueryState(ResultElement result)
        {
            return new GeneralQueryState(result);
        }

        public virtual MappingData GenerateMappingData()
        {
            MappingData data = new MappingData();

            DbSqlQueryExpression sqlQuery = this.CreateSqlQuery();

            var objectActivatorCreator = this._resultElement.MappingObjectExpression.GenarateObjectActivatorCreator(sqlQuery);

            data.SqlQuery = sqlQuery;
            data.ObjectActivatorCreator = objectActivatorCreator;

            return data;
        }

        public virtual GeneralQueryState AsSubQueryState()
        {
            DbSqlQueryExpression sqlQuery = this.CreateSqlQuery();
            DbSubQueryExpression subQuery = new DbSubQueryExpression(sqlQuery);

            ResultElement result = new ResultElement();

            DbTableSegment tableSeg = new DbTableSegment(subQuery, result.GenerateUniqueTableAlias());
            DbFromTableExpression fromTable = new DbFromTableExpression(tableSeg);

            result.FromTable = fromTable;

            DbTable table = new DbTable(tableSeg.Alias);

            //TODO 根据旧的生成新 MappingMembers
            IMappingObjectExpression newMoe = this.Result.MappingObjectExpression.ToNewObjectExpression(sqlQuery, table);
            result.MappingObjectExpression = newMoe;

            //得将 subQuery.SqlQuery.Orders 告诉 以下创建的 result
            //将 orderPart 传递下去
            if (this.Result.Orderings.Count > 0)
            {
                for (int i = 0; i < this.Result.Orderings.Count; i++)
                {
                    DbOrdering ordering = this.Result.Orderings[i];
                    DbExpression orderingExp = ordering.Expression;

                    string alias = null;

                    DbColumnSegment columnExpression = sqlQuery.ColumnSegments.Where(a => DbExpressionEqualityComparer.EqualsCompare(orderingExp, a.Body)).FirstOrDefault();

                    // 对于重复的则不需要往 sqlQuery.Columns 重复添加了
                    if (columnExpression != null)
                    {
                        alias = columnExpression.Alias;
                    }
                    else
                    {
                        alias = Utils.GenerateUniqueColumnAlias(sqlQuery);
                        DbColumnSegment columnSeg = new DbColumnSegment(orderingExp, alias);
                        sqlQuery.ColumnSegments.Add(columnSeg);
                    }

                    DbColumnAccessExpression columnAccessExpression = new DbColumnAccessExpression(table, DbColumn.MakeColumn(orderingExp, alias));
                    result.Orderings.Add(new DbOrdering(columnAccessExpression, ordering.OrderType));
                }
            }

            result.InheritOrderings = true;

            GeneralQueryState queryState = new GeneralQueryState(result);
            return queryState;
        }
        public virtual DbSqlQueryExpression CreateSqlQuery()
        {
            DbSqlQueryExpression sqlQuery = new DbSqlQueryExpression();

            sqlQuery.Table = this._resultElement.FromTable;
            sqlQuery.Orderings.AddRange(this._resultElement.Orderings);
            sqlQuery.Condition = this._resultElement.Condition;

            sqlQuery.GroupSegments.AddRange(this._resultElement.GroupSegments);
            sqlQuery.HavingCondition = this._resultElement.HavingCondition;

            return sqlQuery;
        }

        protected static DbOrdering VisistOrderExpression(List<IMappingObjectExpression> moeList, OrderExpression orderExp)
        {
            DbExpression dbExpression = GeneralExpressionVisitor.ParseLambda(orderExp.KeySelector, moeList);
            DbOrderType orderType;
            if (orderExp.NodeType == QueryExpressionType.OrderBy || orderExp.NodeType == QueryExpressionType.ThenBy)
            {
                orderType = DbOrderType.Asc;
            }
            else if (orderExp.NodeType == QueryExpressionType.OrderByDesc || orderExp.NodeType == QueryExpressionType.ThenByDesc)
            {
                orderType = DbOrderType.Desc;
            }
            else
                throw new NotSupportedException(orderExp.NodeType.ToString());

            DbOrdering ordering = new DbOrdering(dbExpression, orderType);

            return ordering;
        }

        public virtual FromQueryResult ToFromQueryResult()
        {
            DbSqlQueryExpression sqlQuery = this.CreateSqlQuery();
            DbSubQueryExpression subQuery = new DbSubQueryExpression(sqlQuery);

            DbTableSegment tableSeg = new DbTableSegment(subQuery, UtilConstants.DefaultTableAlias);
            DbFromTableExpression fromTable = new DbFromTableExpression(tableSeg);

            DbTable table = new DbTable(tableSeg.Alias);
            IMappingObjectExpression newMoe = this.Result.MappingObjectExpression.ToNewObjectExpression(sqlQuery, table);

            FromQueryResult result = new FromQueryResult();
            result.FromTable = fromTable;
            result.MappingObjectExpression = newMoe;
            return result;
        }

        public virtual JoinQueryResult ToJoinQueryResult(DbJoinType joinType, LambdaExpression conditionExpression, DbFromTableExpression fromTable, List<IMappingObjectExpression> moeList, string tableAlias)
        {
            DbSqlQueryExpression sqlQuery = this.CreateSqlQuery();
            DbSubQueryExpression subQuery = new DbSubQueryExpression(sqlQuery);

            string alias = tableAlias;
            DbTableSegment tableSeg = new DbTableSegment(subQuery, alias);

            DbTable table = new DbTable(tableSeg.Alias);
            IMappingObjectExpression newMoe = this.Result.MappingObjectExpression.ToNewObjectExpression(sqlQuery, table);

            List<IMappingObjectExpression> moes = new List<IMappingObjectExpression>(moeList.Count + 1);
            moes.AddRange(moeList);
            moes.Add(newMoe);
            DbExpression condition = GeneralExpressionVisitor.ParseLambda(conditionExpression, moes);

            DbJoinTableExpression joinTable = new DbJoinTableExpression(joinType, tableSeg, condition);

            JoinQueryResult result = new JoinQueryResult();
            result.MappingObjectExpression = newMoe;
            result.JoinTable = joinTable;
            return result;
        }
    }
}
