using System;
using System.Collections.Generic;
using System.Linq;

namespace _3HashSearch
{
    public class DataList
    {
        private NodeCustom _head;
        private NodeCustom _current;
        private int _chainCount;

        private List<NodeCustom> _list;
        
        public void Add(string key, string value)
        {
            if (_list == null || !_list.Any())
            {
                _list = new List<NodeCustom>(){new NodeCustom(key, value) };
                return;
            }

            _list.Add(new NodeCustom(key, value));
        }

        public bool Update(string key, string value)
        {
            if (_head == null)
            {
                _chainCount++;
                Add(key, value);
                return false;
            }

            var tmp = First();

            while (tmp.Next != null)
            {
                if (tmp.GetKey() == key)
                {
                    tmp.SetValue(value);
                    return true;
                }

                tmp = Next();
            }

            _chainCount++;
            tmp.Next = new NodeCustom(key, value);
            return false;
        }

        public string Get(string key)//10
        {
            var tmp = First();
            if (tmp == null) return null;

            if (tmp.GetKey() == key)
                return tmp.GetValue();

            while (tmp.Next != null)
            {
                tmp = tmp.Next;
                if (tmp.GetKey() == key)
                    return tmp.GetValue();
            }

            return null;
        }

        public string Find(string key)
        {
            if (_list == null)
                return null;

            foreach (var t in _list)
            {
                if (t.GetKey() == key)
                {
                    return t.GetValue();
                }
            }

            return null;
        }

        public string Delete(string key)
        {
            var tmp = First();
            if (tmp == null) return null;

            if (tmp.GetKey() == key)
            {
                _head = tmp.Next;
                _chainCount--;
                return tmp.GetValue();
            }

            var prev = tmp;
            while (tmp.Next != null)
            {
                tmp = tmp.Next;
                if (tmp.GetKey() != key) continue;

                prev.Next = tmp.Next;
                _chainCount--;
                return tmp.GetValue();
            }

            return null;
        }

        public NodeCustom First()
        {
            _current = _head;
            return _current;
        }

        public NodeCustom Next()
        {
            _current = _current.Next;
            return _current;
        }

        public int GetChainCount()
        {
            return _chainCount;
        }

        public string PrintOutChain()
        {
            var nodeChain = "";
            var current = First();
            while (current != null)
            {
                nodeChain += $" >>> {current.GetKey()}={current.GetKeyHash()}";
                current = Next();
            }

            return nodeChain;
        }

        public class NodeCustom
        {
            private readonly string _key;
            private string _value;
            // Ref to next NodeCustom
            public NodeCustom Next;

            public NodeCustom(NodeCustom next)
            {
                Next = next;
            }

            public NodeCustom(string key, string value)
            {
                _key = key;
                _value = value;
            }

            public string GetKey()
            {
                return _key;
            }

            public string GetValue()
            {
                return _value;
            }

            public void SetValue(string value)
            {
                _value = value;
            }

            public int GetKeyHash()
            {
                return Math.Abs(_key.GetHashCode());
            }

            public override string ToString()
            {
                return _key + "=" + _value;
            }
        }
    }
}