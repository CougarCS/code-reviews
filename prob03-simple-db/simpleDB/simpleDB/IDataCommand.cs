using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simpleDB
{
    interface IDataCommand
    {
        string Message { get; }
        int? Operation(List<string> args);
    }
}
