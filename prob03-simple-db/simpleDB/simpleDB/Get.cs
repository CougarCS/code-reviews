using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simpleDB
{
    public class Get : IDataCommand
    {
        public int? Operation(List<object> args)
        {
            try
            {
                return Memory.Data[(string)args[0]];
            }
            catch
            {
                return null;
            }
        }
    }
}
