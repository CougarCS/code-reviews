using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simpleDB
{
    public class Rollback : IDataCommand
    {
        public string Message { get; private set; }
        public int? Operation(List<string> args)
        {
            Transaction.Rollback();
            return null;
        }
    }
}
