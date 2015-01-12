using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simpleDB
{
    public class Set : IDataCommand
    {
        public string Message { get; private set; }
        public Set()
        {
            Message = Messages.DEFAULT_MESSAGE;
        }
        public int? Operation(List<string> args)
        {
            Message = Messages.DEFAULT_MESSAGE;
            string name = args[0];
            int value;

            try
            {
                value = int.Parse(args[1]);
            }
            catch
            {
                Message = Messages.INVALID_ARGUMENT;
                return null;
            }

            if (Memory.Data.ContainsKey(name))
                Memory.Data[name] = value;
            else
                Memory.Data.Add(name, value);

            return null;
        }
    }
}
