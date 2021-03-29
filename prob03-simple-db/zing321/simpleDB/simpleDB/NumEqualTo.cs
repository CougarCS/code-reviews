using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simpleDB
{
    public class NumEqualTo : IDataCommand
    {
        public string Message { get; private set; }
        public NumEqualTo()
        {
            Message = Messages.DEFAULT_MESSAGE;
        }
        public int? Operation(List<string> args)
        {
            Message = Messages.DEFAULT_MESSAGE;
            int checkVal = 0;
            try
            {
                checkVal = int.Parse(args[0]);
            }
            catch
            {
                Message = Messages.INVALID_ARGUMENT;
                return null;
            }
            List<int> matches = Memory.Data.Values.ToList().FindAll(x => x == checkVal);
            
            return matches.Count;
        }
    }
}
