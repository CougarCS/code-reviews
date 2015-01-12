using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simpleDB
{
    interface IDataCommand
    {
        int? Operation(List<object> args);
    }
}
