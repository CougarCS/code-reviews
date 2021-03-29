using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simpleDB
{
    delegate void KeyValueChangedHandeler<TKey, TValue>(object sender, KeyValueChangedArgs<TKey, TValue> e);

    class KeyValueChangedArgs<TKey, TValue> : EventArgs
    {
        public TKey Key { get; private set; }
        public TValue Value { get; private set; }
        public TValue PreviousValue { get; private set; }
        public bool Removed { get; private set; }
        public bool NewEntry { get; private set; }

        public KeyValueChangedArgs(TKey key, TValue value, TValue previousValue, bool removed, bool newEntry)
        {
            Key = key;
            Value = value;
            Removed = removed;
            PreviousValue = previousValue;
            NewEntry = newEntry;
        }
    }

    class EventDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public event KeyValueChangedHandeler<TKey, TValue> KeyValueChanged;

        public new void Add(TKey key, TValue value)
        {
            base.Add(key, value);
            FireKeyValueChangedEvent(key, value, default(TValue), false, true);           
        }
        public new bool Remove(TKey key)
        {
            TValue previousValue = this[key];
            bool pass = base.Remove(key);
            FireKeyValueChangedEvent(key, default(TValue), previousValue, true, false);

            return pass;
        }

        public new TValue this[TKey key]
        {
            get
            {
                return base[key];
            }
            set
            {
                if (this.ContainsKey(key))
                {
                    TValue previousValue = base[key];
                    base[key] = value;
                    FireKeyValueChangedEvent(key, value, previousValue, false, false);
                }
                else
                {
                    base[key] = value;
                    FireKeyValueChangedEvent(key, value, default(TValue), false, true);
                }
            }
        }

        private void FireKeyValueChangedEvent(TKey key, TValue value, TValue previousValue, bool removed, bool newEntry)
        {
            if (KeyValueChanged != null)
                KeyValueChanged(this, new KeyValueChangedArgs<TKey, TValue>(key, value,previousValue, removed, newEntry));
        }
    }
}
