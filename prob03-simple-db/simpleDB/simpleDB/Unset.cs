using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simpleDB
{
    public class Unset : IDataCommand
    {
        public string Message { get; private set; }
        public Unset()
        {
            Message = Messages.DEFAULT_MESSAGE;
        }
        public int? Operation(List<string> args)
        {
            Memory.Data.Remove(args[0]);
            return null;
        }
    }
}
