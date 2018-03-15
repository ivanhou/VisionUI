using Chloe.DbExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chloe.Query
{
    class FromQueryResult
    {
        public DbFromTableExpression FromTable { get; set; }
        public IMappingObjectExpression MappingObjectExpression { get; set; }
    }
}
