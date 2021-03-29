using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simpleDB
{
    public class End : IDataCommand
    {
        public string Message { get; private set; }
        public End()
        {
            Message = Messages.DEFAULT_MESSAGE;
        }
        public int? Operation(List<string> args)
        {
            Environment.Exit(0);
            return null;
        }
    }
}
