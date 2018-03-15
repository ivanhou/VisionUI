using Chloe.Utility;

namespace Chloe.DbExpressions
{
    [System.Diagnostics.DebuggerDisplay("Name = {Name}")]
    public class DbTable
    {
        string _name;
        public DbTable(string name)
        {
            this._name = name;
        }

        public string Name { get { return this._name; } }
    }
}
