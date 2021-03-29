using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simpleDB
{
    public class ResetAll : IDataCommand
    {
        public string Message { get; private set; }
        public ResetAll()
        {
            Message = Messages.DEFAULT_MESSAGE;
        }
        public int? Operation(List<string> args)
        {
            Memory.Reset();
            return null;
        }
    }
}
