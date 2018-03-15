using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Chloe.Mapper
{
    public class NavigationMemberBinder : IValueSetter
    {
        Action<object, object> _setter;
        IObjectActivator _activtor;
        public NavigationMemberBinder(Action<object, object> setter, IObjectActivator activtor)
        {
            this._setter = setter;
            this._activtor = activtor;
        }
        public void SetValue(object obj, IDataReader reader)
        {
            object val = this._activtor.CreateInstance(reader);
            this._setter(obj, val);
        }
    }
}
