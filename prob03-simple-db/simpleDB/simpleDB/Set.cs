using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simpleDB
{
    public class Set : IDataCommand
    {
        public int? Operation(List<object> args)
        {
            string name = (string)args[0];
            int value = (int)args[1];

            if (Memory.Data.ContainsKey(name))
                Memory.Data[name] = value;
            else
                Memory.Data.Add(name, value);

            return null;
        }
    }
}
