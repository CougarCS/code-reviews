using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simpleDB
{
    public class Get : IDataCommand
    {
        public string Message { get; private set; }
        public Get()
        {
            Message = Messages.DEFAULT_MESSAGE;
        }
        public int? Operation(List<string> args)
        {
            Message = Messages.DEFAULT_MESSAGE;
            try
            {
                return Memory.Data[(string)args[0]];
            }
            catch
            {
                Message = Messages.NULL;
                return null;
            }
        }
    }
}
