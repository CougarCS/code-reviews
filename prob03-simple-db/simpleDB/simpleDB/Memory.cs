using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simpleDB
{
    static class Memory
    {
        //data is exposed to data commands
        //making this public allows more elaborate commands without having to add necissary functions to the Memory class
        public static Dictionary<string, int> Data { get; set; }

        public static void Reset()
        {
            Data = new Dictionary<string, int>();
        }
    }
}
