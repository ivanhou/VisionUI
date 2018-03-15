using Chloe.Infrastructure.Interception;
using System;
using System.Data;

namespace Chloe
{
    public interface IDbSession : IDisposable
    {
        IDbContext DbContext { get; }
        bool IsInTransaction { get; }
        int CommandTimeout { get; set; }

        int ExecuteNonQuery(string cmdText, params DbParam[] parameters);
        int ExecuteNonQuery(string cmdText, CommandType cmdType, params DbParam[] parameters);

        object ExecuteScalar(string cmdText, params DbParam[] parameters);
        object ExecuteScalar(string cmdText, CommandType cmdType, params DbParam[] parameters);

        IDataReader ExecuteReader(string cmdText, params DbParam[] parameters);
        IDataReader ExecuteReader(string cmdText, CommandType cmdType, params DbParam[] parameters);

        void BeginTransaction();
        void BeginTransaction(IsolationLevel il);
        void CommitTransaction();
        void RollbackTransaction();

        void AddInterceptor(IDbCommandInterceptor interceptor);
        void RemoveInterceptor(IDbCommandInterceptor interceptor);
    }
}
