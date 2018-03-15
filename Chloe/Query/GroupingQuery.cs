using Chloe.Core;
using Chloe.Query.QueryExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Chloe.Query
{
    class GroupingQuery<T> : IGroupingQuery<T>
    {
        Query<T> _fromQuery;
        List<LambdaExpression> _groupKeySelectors;
        List<LambdaExpression> _havingPredicates;

        public GroupingQuery(Query<T> fromQuery, LambdaExpression keySelector)
        {
            this._fromQuery = fromQuery;
            this._groupKeySelectors = new List<LambdaExpression>(1);
            this._havingPredicates = new List<LambdaExpression>();

            this._groupKeySelectors.Add(keySelector);
        }
        public GroupingQuery(Query<T> fromQuery, List<LambdaExpression> groupKeySelectors, List<LambdaExpression> havingPredicates)
        {
            this._fromQuery = fromQuery;
            this._groupKeySelectors = groupKeySelectors;
            this._havingPredicates = havingPredicates;
        }

        public IGroupingQuery<T> AndBy<K>(Expression<Func<T, K>> keySelector)
        {
            List<LambdaExpression> groupKeySelectors = new List<LambdaExpression>(this._groupKeySelectors.Count + 1);
            groupKeySelectors.AddRange(this._groupKeySelectors);
            groupKeySelectors.Add(keySelector);

            List<LambdaExpression> havingPredicates = new List<LambdaExpression>(this._havingPredicates.Count);
            havingPredicates.AddRange(this._havingPredicates);

            return new GroupingQuery<T>(this._fromQuery, groupKeySelectors, havingPredicates);
        }
        public IGroupingQuery<T> Having(Expression<Func<T, bool>> predicate)
        {
            List<LambdaExpression> groupKeySelectors = new List<LambdaExpression>(this._groupKeySelectors.Count);
            groupKeySelectors.AddRange(this._groupKeySelectors);

            List<LambdaExpression> havingPredicates = new List<LambdaExpression>(this._havingPredicates.Count + 1);
            havingPredicates.AddRange(this._havingPredicates);
            havingPredicates.Add(predicate);

            return new GroupingQuery<T>(this._fromQuery, groupKeySelectors, havingPredicates);
        }
        public IQuery<TResult> Select<TResult>(Expression<Func<T, TResult>> selector)
        {
            var e = new GroupingQueryExpression(typeof(TResult), this._fromQuery.QueryExpression, selector);
            e.GroupKeySelectors.AddRange(this._groupKeySelectors);
            e.HavingPredicates.AddRange(this._havingPredicates);
            return new Query<TResult>(this._fromQuery.DbContext, e, this._fromQuery._trackEntity);
        }
    }
}
