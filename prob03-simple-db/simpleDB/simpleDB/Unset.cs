using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simpleDB
{
    public class Unset : IDataCommand
    {
        public int? Operation(List<object> args)
        {
            Memory.Data.Remove((string)args[0]);
            return null;
        }
    }
}
