using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simpleDB
{
    public class Begin : IDataCommand
    {
        public string Message { get; private set; }
        public Begin()
        {
            Message = Messages.DEFAULT_MESSAGE;
        }
        public int? Operation(List<string> args)
        {
            Transaction.BeginTransaction();
            return null;
        }
    }
}
