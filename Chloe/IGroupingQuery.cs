using System;
using System.Linq.Expressions;

namespace Chloe
{
    public interface IGroupingQuery<T>
    {
        IGroupingQuery<T> AndBy<K>(Expression<Func<T, K>> keySelector);
        IGroupingQuery<T> Having(Expression<Func<T, bool>> predicate);
        IQuery<TResult> Select<TResult>(Expression<Func<T, TResult>> selector);
    }
}
