using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simpleDB
{
    public class ResetAll : IDataCommand
    {
        public int? Operation(List<object> args)
        {
            Memory.Reset();
            return null;
        }
    }
}
