using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simpleDB
{
    public class NumEqualTo : IDataCommand
    {
        public int? Operation(List<object> args)
        {
            int checkVal = (int)args[0];
            List<int> matches = Memory.Data.Values.ToList().FindAll(x => x == checkVal);
            
            return matches.Count;
        }
    }
}
