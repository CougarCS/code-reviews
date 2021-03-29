using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simpleDB
{
    class MemoryStorageNode<Tvalue>
    {
        public Tvalue Value { get; set; }
        public Tvalue PreviousValue { get; set; }
        public bool PreviousValueRecorded { get; set; }
        public MemoryStorageNode()
        {
            PreviousValueRecorded = false;
        }
    }

    //difficult to implement transaction commands in their own IDataCommand class without exposing the entire transaction
    static class Transaction
    {
        public static bool IsTransactionActive { get { return _nodes.Count > 0; } }
        static Stack<Dictionary<string, MemoryStorageNode<int?>>> _nodes = new Stack<Dictionary<string, MemoryStorageNode<int?>>>();

        public static void BeginTransaction()
        {
            if (_nodes.Count == 0)
                Memory.Data.KeyValueChanged += Data_KeyValueChanged;

            _nodes.Push(new Dictionary<string, MemoryStorageNode<int?>>());
        }

        public static void Commit()
        {
            var commitNode = _nodes.Pop();

            if(_nodes.Count >= 1)
            {
                var topNode = _nodes.Peek();
                foreach(var key in commitNode.Keys)
                {
                    //retain previous value of the top node
                    if(topNode.ContainsKey(key))
                        commitNode[key].PreviousValue = topNode[key].PreviousValue;

                    topNode[key] = commitNode[key];
                }
            }

            EndTransaction();
        }

        public static void Rollback()
        {
            var rollbackNode = _nodes.Pop();
            Memory.Data.KeyValueChanged -= Data_KeyValueChanged;

            foreach(var key in rollbackNode.Keys)
            {
                if (!rollbackNode[key].PreviousValue.HasValue)
                    Memory.Data.Remove(key);
                else
                    Memory.Data[key] = rollbackNode[key].PreviousValue.Value;
            }

            Memory.Data.KeyValueChanged += Data_KeyValueChanged;
            EndTransaction();
        }

        static void EndTransaction()
        {
            if (_nodes.Count == 0)
                Memory.Data.KeyValueChanged -= Data_KeyValueChanged;
        }

        static void Data_KeyValueChanged(object sender, KeyValueChangedArgs<string, int> e)
        {
            var topNode = _nodes.Peek();

            if (!topNode.ContainsKey(e.Key))
                topNode[e.Key] = new MemoryStorageNode<int?>();

            if(!topNode[e.Key].PreviousValueRecorded)
            {
                if (e.NewEntry)
                    topNode[e.Key].PreviousValue = null;
                else
                    topNode[e.Key].PreviousValue = e.PreviousValue;

                topNode[e.Key].PreviousValueRecorded = true;
            }

            if (e.Removed)
                    topNode[e.Key].Value = null;
                else
                    topNode[e.Key].Value = e.Value;
        }
    }
}
