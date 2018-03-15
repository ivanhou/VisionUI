using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace Chloe
{
    public interface IDbContext : IDisposable
    {
        IDbSession Session { get; }

        IQuery<TEntity> Query<TEntity>();
        TEntity QueryByKey<TEntity>(object key, bool tracking = false);

        IEnumerable<T> SqlQuery<T>(string sql, params DbParam[] parameters);
        IEnumerable<T> SqlQuery<T>(string sql, CommandType cmdType, params DbParam[] parameters);

        TEntity Insert<TEntity>(TEntity entity);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="body"></param>
        /// <returns>PrimaryKey</returns>
        object Insert<TEntity>(Expression<Func<TEntity>> body);

        int Update<TEntity>(TEntity entity);
        int Update<TEntity>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TEntity>> body);

        int Delete<TEntity>(TEntity entity);
        int Delete<TEntity>(Expression<Func<TEntity, bool>> condition);
        int DeleteByKey<TEntity>(object key);

        void TrackEntity(object entity);
    }
}
