using Chloe.Core;
using Chloe.Core.Visitors;
using Chloe.DbExpressions;
using Chloe.Descriptors;
using Chloe.Entity;
using Chloe.Exceptions;
using Chloe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Chloe.SQLite
{
    public class SQLiteContext : DbContext
    {
        DbContextServiceProvider _dbContextServiceProvider;
        public SQLiteContext(IDbConnectionFactory dbConnectionFactory)
        {
            Utils.CheckNull(dbConnectionFactory);

            this._dbContextServiceProvider = new DbContextServiceProvider(dbConnectionFactory);
        }

        public override IDbContextServiceProvider DbContextServiceProvider
        {
            get { return this._dbContextServiceProvider; }
        }
        protected override string GetSelectLastInsertIdClause()
        {
            return "SELECT LAST_INSERT_ROWID()";
        }
    }
}
