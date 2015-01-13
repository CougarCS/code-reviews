using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simpleDB
{
    static class Transaction
    {
        static bool _isTransactionActive = false;
        static Dictionary<string,int> _dataStore;
        static List<Dictionary<string, int>> _nodes = new List<Dictionary<string, int>>();

        public static void StageData()
        {
            if (!_isTransactionActive)
                _dataStore = Memory.Data;
            else
                _nodes.Add(Memory.Data);

            Memory.Reset();
            _isTransactionActive = true;
        }
        
        public static void FetchName(string name)
        {

        }
    }
}
