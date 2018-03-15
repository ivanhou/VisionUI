using Chloe.Infrastructure.Interception;
using Chloe.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Chloe.Core
{
    class DbSession : IDbSession
    {
        DbContext _dbContext;
        internal DbSession(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IDbContext DbContext { get { return this._dbContext; } }
        public bool IsInTransaction { get { return this._dbContext.InnerDbSession.IsInTransaction; } }
        public int CommandTimeout { get { return this._dbContext.InnerDbSession.CommandTimeout; } set { this._dbContext.InnerDbSession.CommandTimeout = value; } }

        public int ExecuteNonQuery(string cmdText, params DbParam[] parameters)
        {
            return this.ExecuteNonQuery(cmdText, CommandType.Text, parameters);
        }
        public int ExecuteNonQuery(string cmdText, CommandType cmdType, params DbParam[] parameters)
        {
            Utils.CheckNull(cmdText, "cmdText");
            return this._dbContext.InnerDbSession.ExecuteNonQuery(cmdText, parameters, cmdType);
        }

        public object ExecuteScalar(string cmdText, params DbParam[] parameters)
        {
            return this.ExecuteScalar(cmdText, CommandType.Text, parameters);
        }
        public object ExecuteScalar(string cmdText, CommandType cmdType, params DbParam[] parameters)
        {
            Utils.CheckNull(cmdText, "cmdText");
            return this._dbContext.InnerDbSession.ExecuteScalar(cmdText, parameters, cmdType);
        }

        public IDataReader ExecuteReader(string cmdText, params DbParam[] parameters)
        {
            return this.ExecuteReader(cmdText, CommandType.Text, parameters);
        }
        public IDataReader ExecuteReader(string cmdText, CommandType cmdType, params DbParam[] parameters)
        {
            Utils.CheckNull(cmdText, "cmdText");
            return this._dbContext.InnerDbSession.ExecuteReader(cmdText, parameters, cmdType);
        }

        /* Using IsolationLevel.ReadCommitted level.  */
        public void BeginTransaction()
        {
            this.BeginTransaction(IsolationLevel.ReadCommitted);
        }
        public void BeginTransaction(IsolationLevel il)
        {
            this._dbContext.InnerDbSession.BeginTransaction(il);
        }
        public void CommitTransaction()
        {
            this._dbContext.InnerDbSession.CommitTransaction();
        }
        public void RollbackTransaction()
        {
            this._dbContext.InnerDbSession.RollbackTransaction();
        }

        public void AddInterceptor(IDbCommandInterceptor interceptor)
        {
            Utils.CheckNull(interceptor, "interceptor");
            this._dbContext.InnerDbSession.DbCommandInterceptors.Add(interceptor);
        }
        public void RemoveInterceptor(IDbCommandInterceptor interceptor)
        {
            Utils.CheckNull(interceptor, "interceptor");
            this._dbContext.InnerDbSession.DbCommandInterceptors.Remove(interceptor);
        }

        public void Dispose()
        {
            this._dbContext.Dispose();
        }
    }
}
