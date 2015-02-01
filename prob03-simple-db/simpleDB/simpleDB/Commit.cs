using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simpleDB
{
    public class Commit : IDataCommand
    {
        public string Message { get; private set; }
        public int? Operation(List<string> args)
        {
            Transaction.Commit();
            return null;
        }
    }
}
