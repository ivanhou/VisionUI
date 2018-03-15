using Chloe.Query.QueryExpressions;
using Chloe.Query.Mapping;
using Chloe.Query.Visitors;
using Chloe.DbExpressions;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Chloe.Query.QueryState
{
    interface IQueryState
    {
        MappingData GenerateMappingData();

        FromQueryResult ToFromQueryResult();
        JoinQueryResult ToJoinQueryResult(DbJoinType joinType, LambdaExpression conditionExpression, DbFromTableExpression fromTable, List<IMappingObjectExpression> moeList, string tableAlias);

        IQueryState Accept(WhereExpression exp);
        IQueryState Accept(OrderExpression exp);
        IQueryState Accept(SelectExpression exp);
        IQueryState Accept(SkipExpression exp);
        IQueryState Accept(TakeExpression exp);
        IQueryState Accept(AggregateQueryExpression exp);
        IQueryState Accept(GroupingQueryExpression exp);
    }
}
