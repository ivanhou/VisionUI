using Chloe.Core;
using Chloe.DbExpressions;
using Chloe.Infrastructure;
using Chloe.Query.QueryExpressions;
using Chloe.Query.QueryState;
using Chloe.Query.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Chloe.Query
{
    class JoiningQuery<T1, T2> : IJoiningQuery<T1, T2>
    {
        DbContext _dbContext;

        QueryBase _rootQuery;
        List<JoiningQueryInfo> _joinedQueries;

        public DbContext DbContext { get { return this._dbContext; } }
        public QueryBase RootQuery { get { return this._rootQuery; } }
        public List<JoiningQueryInfo> JoinedQueries { get { return this._joinedQueries; } }

        public JoiningQuery(Query<T1> q1, Query<T2> q2, DbJoinType joinType, Expression<Func<T1, T2, bool>> on)
        {
            this._dbContext = q1.DbContext;
            this._rootQuery = q1;
            this._joinedQueries = new List<JoiningQueryInfo>(1);

            JoiningQueryInfo joiningQueryInfo = new JoiningQueryInfo(q2, joinType, on);
            this._joinedQueries.Add(joiningQueryInfo);
        }

        public IJoiningQuery<T1, T2, T3> InnerJoin<T3>(Expression<Func<T1, T2, T3, bool>> on)
        {
            return this.InnerJoin<T3>(this._dbContext.Query<T3>(), on);
        }
        public IJoiningQuery<T1, T2, T3> LeftJoin<T3>(Expression<Func<T1, T2, T3, bool>> on)
        {
            return this.LeftJoin<T3>(this._dbContext.Query<T3>(), on);
        }
        public IJoiningQuery<T1, T2, T3> RightJoin<T3>(Expression<Func<T1, T2, T3, bool>> on)
        {
            return this.RightJoin<T3>(this._dbContext.Query<T3>(), on);
        }
        public IJoiningQuery<T1, T2, T3> FullJoin<T3>(Expression<Func<T1, T2, T3, bool>> on)
        {
            return this.FullJoin<T3>(this._dbContext.Query<T3>(), on);
        }

        public IJoiningQuery<T1, T2, T3> InnerJoin<T3>(IQuery<T3> q, Expression<Func<T1, T2, T3, bool>> on)
        {
            return new JoiningQuery<T1, T2, T3>(this, (Query<T3>)q, DbJoinType.InnerJoin, on);
        }
        public IJoiningQuery<T1, T2, T3> LeftJoin<T3>(IQuery<T3> q, Expression<Func<T1, T2, T3, bool>> on)
        {
            return new JoiningQuery<T1, T2, T3>(this, (Query<T3>)q, DbJoinType.LeftJoin, on);
        }
        public IJoiningQuery<T1, T2, T3> RightJoin<T3>(IQuery<T3> q, Expression<Func<T1, T2, T3, bool>> on)
        {
            return new JoiningQuery<T1, T2, T3>(this, (Query<T3>)q, DbJoinType.RightJoin, on);
        }
        public IJoiningQuery<T1, T2, T3> FullJoin<T3>(IQuery<T3> q, Expression<Func<T1, T2, T3, bool>> on)
        {
            return new JoiningQuery<T1, T2, T3>(this, (Query<T3>)q, DbJoinType.FullJoin, on);
        }
        public IQuery<TResult> Select<TResult>(Expression<Func<T1, T2, TResult>> selector)
        {
            JoinQueryExpression e = new JoinQueryExpression(typeof(TResult), this._rootQuery.QueryExpression, this._joinedQueries, selector);
            return new Query<TResult>(this.DbContext, e, this._rootQuery.TrackEntity);
        }
    }

    class JoiningQuery<T1, T2, T3> : IJoiningQuery<T1, T2, T3>
    {
        DbContext _dbContext;

        QueryBase _rootQuery;
        List<JoiningQueryInfo> _joinedQueries;

        public DbContext DbContext { get { return this._dbContext; } }
        public QueryBase RootQuery { get { return this._rootQuery; } }
        public List<JoiningQueryInfo> JoinedQueries { get { return this._joinedQueries; } }

        public JoiningQuery(JoiningQuery<T1, T2> joiningQuery, Query<T3> q, DbJoinType joinType, Expression<Func<T1, T2, T3, bool>> on)
        {
            this._dbContext = joiningQuery.DbContext;
            this._rootQuery = joiningQuery.RootQuery;
            this._joinedQueries = new List<JoiningQueryInfo>(joiningQuery.JoinedQueries.Count);

            this._joinedQueries.AddRange(joiningQuery.JoinedQueries);

            JoiningQueryInfo joiningQueryInfo = new JoiningQueryInfo(q, joinType, on);
            this._joinedQueries.Add(joiningQueryInfo);
        }

        public IJoiningQuery<T1, T2, T3, T4> InnerJoin<T4>(Expression<Func<T1, T2, T3, T4, bool>> on)
        {
            return this.InnerJoin<T4>(this._dbContext.Query<T4>(), on);
        }
        public IJoiningQuery<T1, T2, T3, T4> LeftJoin<T4>(Expression<Func<T1, T2, T3, T4, bool>> on)
        {
            return this.LeftJoin<T4>(this._dbContext.Query<T4>(), on);
        }
        public IJoiningQuery<T1, T2, T3, T4> RightJoin<T4>(Expression<Func<T1, T2, T3, T4, bool>> on)
        {
            return this.RightJoin<T4>(this._dbContext.Query<T4>(), on);
        }
        public IJoiningQuery<T1, T2, T3, T4> FullJoin<T4>(Expression<Func<T1, T2, T3, T4, bool>> on)
        {
            return this.FullJoin<T4>(this._dbContext.Query<T4>(), on);
        }

        public IJoiningQuery<T1, T2, T3, T4> InnerJoin<T4>(IQuery<T4> q, Expression<Func<T1, T2, T3, T4, bool>> on)
        {
            return new JoiningQuery<T1, T2, T3, T4>(this, (Query<T4>)q, DbJoinType.InnerJoin, on);
        }
        public IJoiningQuery<T1, T2, T3, T4> LeftJoin<T4>(IQuery<T4> q, Expression<Func<T1, T2, T3, T4, bool>> on)
        {
            return new JoiningQuery<T1, T2, T3, T4>(this, (Query<T4>)q, DbJoinType.LeftJoin, on);
        }
        public IJoiningQuery<T1, T2, T3, T4> RightJoin<T4>(IQuery<T4> q, Expression<Func<T1, T2, T3, T4, bool>> on)
        {
            return new JoiningQuery<T1, T2, T3, T4>(this, (Query<T4>)q, DbJoinType.RightJoin, on);
        }
        public IJoiningQuery<T1, T2, T3, T4> FullJoin<T4>(IQuery<T4> q, Expression<Func<T1, T2, T3, T4, bool>> on)
        {
            return new JoiningQuery<T1, T2, T3, T4>(this, (Query<T4>)q, DbJoinType.FullJoin, on);
        }

        public IQuery<TResult> Select<TResult>(Expression<Func<T1, T2, T3, TResult>> selector)
        {
            JoinQueryExpression e = new JoinQueryExpression(typeof(TResult), this._rootQuery.QueryExpression, this._joinedQueries, selector);
            return new Query<TResult>(this.DbContext, e, this._rootQuery.TrackEntity);
        }
    }

    class JoiningQuery<T1, T2, T3, T4> : IJoiningQuery<T1, T2, T3, T4>
    {
        DbContext _dbContext;

        QueryBase _rootQuery;
        List<JoiningQueryInfo> _joinedQueries;

        public DbContext DbContext { get { return this._dbContext; } }
        public QueryBase RootQuery { get { return this._rootQuery; } }
        public List<JoiningQueryInfo> JoinedQueries { get { return this._joinedQueries; } }

        public JoiningQuery(JoiningQuery<T1, T2, T3> joiningQuery, Query<T4> q, DbJoinType joinType, Expression<Func<T1, T2, T3, T4, bool>> on)
        {
            this._dbContext = joiningQuery.DbContext;
            this._rootQuery = joiningQuery.RootQuery;
            this._joinedQueries = new List<JoiningQueryInfo>(joiningQuery.JoinedQueries.Count);

            this._joinedQueries.AddRange(joiningQuery.JoinedQueries);

            JoiningQueryInfo joiningQueryInfo = new JoiningQueryInfo(q, joinType, on);
            this._joinedQueries.Add(joiningQueryInfo);
        }

        public IJoiningQuery<T1, T2, T3, T4, T5> InnerJoin<T5>(Expression<Func<T1, T2, T3, T4, T5, bool>> on)
        {
            return this.InnerJoin<T5>(this._dbContext.Query<T5>(), on);
        }
        public IJoiningQuery<T1, T2, T3, T4, T5> LeftJoin<T5>(Expression<Func<T1, T2, T3, T4, T5, bool>> on)
        {
            return this.LeftJoin<T5>(this._dbContext.Query<T5>(), on);
        }
        public IJoiningQuery<T1, T2, T3, T4, T5> RightJoin<T5>(Expression<Func<T1, T2, T3, T4, T5, bool>> on)
        {
            return this.RightJoin<T5>(this._dbContext.Query<T5>(), on);
        }
        public IJoiningQuery<T1, T2, T3, T4, T5> FullJoin<T5>(Expression<Func<T1, T2, T3, T4, T5, bool>> on)
        {
            return this.FullJoin<T5>(this._dbContext.Query<T5>(), on);
        }

        public IJoiningQuery<T1, T2, T3, T4, T5> InnerJoin<T5>(IQuery<T5> q, Expression<Func<T1, T2, T3, T4, T5, bool>> on)
        {
            return new JoiningQuery<T1, T2, T3, T4, T5>(this, (Query<T5>)q, DbJoinType.InnerJoin, on);
        }
        public IJoiningQuery<T1, T2, T3, T4, T5> LeftJoin<T5>(IQuery<T5> q, Expression<Func<T1, T2, T3, T4, T5, bool>> on)
        {
            return new JoiningQuery<T1, T2, T3, T4, T5>(this, (Query<T5>)q, DbJoinType.LeftJoin, on);
        }
        public IJoiningQuery<T1, T2, T3, T4, T5> RightJoin<T5>(IQuery<T5> q, Expression<Func<T1, T2, T3, T4, T5, bool>> on)
        {
            return new JoiningQuery<T1, T2, T3, T4, T5>(this, (Query<T5>)q, DbJoinType.RightJoin, on);
        }
        public IJoiningQuery<T1, T2, T3, T4, T5> FullJoin<T5>(IQuery<T5> q, Expression<Func<T1, T2, T3, T4, T5, bool>> on)
        {
            return new JoiningQuery<T1, T2, T3, T4, T5>(this, (Query<T5>)q, DbJoinType.FullJoin, on);
        }

        public IQuery<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, TResult>> selector)
        {
            JoinQueryExpression e = new JoinQueryExpression(typeof(TResult), this._rootQuery.QueryExpression, this._joinedQueries, selector);
            return new Query<TResult>(this.DbContext, e, this._rootQuery.TrackEntity);
        }
    }

    class JoiningQuery<T1, T2, T3, T4, T5> : IJoiningQuery<T1, T2, T3, T4, T5>
    {
        DbContext _dbContext;

        QueryBase _rootQuery;
        List<JoiningQueryInfo> _joinedQueries;

        public DbContext DbContext { get { return this._dbContext; } }
        public QueryBase RootQuery { get { return this._rootQuery; } }
        public List<JoiningQueryInfo> JoinedQueries { get { return this._joinedQueries; } }

        public JoiningQuery(JoiningQuery<T1, T2, T3, T4> joiningQuery, Query<T5> q, DbJoinType joinType, Expression<Func<T1, T2, T3, T4, T5, bool>> on)
        {
            this._dbContext = joiningQuery.DbContext;
            this._rootQuery = joiningQuery.RootQuery;
            this._joinedQueries = new List<JoiningQueryInfo>(joiningQuery.JoinedQueries.Count);

            this._joinedQueries.AddRange(joiningQuery.JoinedQueries);

            JoiningQueryInfo joiningQueryInfo = new JoiningQueryInfo(q, joinType, on);
            this._joinedQueries.Add(joiningQueryInfo);
        }

        public IQuery<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, TResult>> selector)
        {
            JoinQueryExpression e = new JoinQueryExpression(typeof(TResult), this._rootQuery.QueryExpression, this._joinedQueries, selector);
            return new Query<TResult>(this.DbContext, e, this._rootQuery.TrackEntity);
        }
    }

}
