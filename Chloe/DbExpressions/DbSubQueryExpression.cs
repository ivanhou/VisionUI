using Chloe.Utility;

namespace Chloe.DbExpressions
{
    public class DbSubQueryExpression : DbExpression
    {
        DbSqlQueryExpression _sqlQuery;

        public DbSubQueryExpression(DbSqlQueryExpression sqlQuery)
            : base(DbExpressionType.SubQuery, UtilConstants.TypeOfVoid)
        {
            this._sqlQuery = sqlQuery;
        }

        public DbSqlQueryExpression SqlQuery { get { return this._sqlQuery; } }

        public override T Accept<T>(DbExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

    }
}
