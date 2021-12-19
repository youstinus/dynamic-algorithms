using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace _3HashSearch
{
    public class HashTable
    {
        public readonly List<DataList.NodeCustom>[] _table;
        private readonly int _length;
        private int _itemCount;
        private readonly SHA256 _sha;

        public HashTable(int size)
        {
            _length = size;
            _table = new List<DataList.NodeCustom>[size];
            _sha = SHA256.Create();
        }

        public int GetTableSize()
        {
            return _table.Length;
        }

        public int GetElementCount()
        {
            return _itemCount;
        }

        public int GetUsedSpacesCount()
        {
            return _table.Count(node => node != null);
        }

        public string Get(string key)
        {
            return _table[GetPosition(key)]?.Find(x => x.GetKey() == key).GetValue();
        }
        
        public void Add(string key, string value)
        {
            if (value == null || key == null)
                throw new InvalidOperationException("Key or value is null in put(Key key, Value value)");
            
            var position = GetPosition(key);
            //Console.WriteLine("{0} {1}", key, position);
            var list = _table[position];
            var node = new DataList.NodeCustom(key, value);
            if (list == null)
            {
                _table[position] = new List<DataList.NodeCustom>(){node};
            }
            else
            {
                _table[position].Add(node);

            }

            _itemCount++;
        }

        /*public void Update(string key, string value)
        {
            if (value == null || key == null)
                throw new InvalidOperationException("Key or value is null in put(Key key, Value value)");

            var position = GetPosition(key);
            var list = GetList(position);
            var updated = list.Update(key, value);

            if (!updated)
                _itemCount++;
        }

        public string Delete(string key)
        {
            var position = GetPosition(key);
            var list = GetList(position);
            return list.Delete(key);
        }*/

        private int GetPosition(string key)//12
        {
            var position = Hash(key);
            return Math.Abs(position) % _length;
        }

        private List<DataList.NodeCustom> GetList(int position)//5
        {
            return _table[position];
        }

        private int Hash(string key)
        {
            return key.GetHashCode();
        }

        private int Hash2(string value)//5
        {
            var bytes = _sha.ComputeHash(Encoding.UTF8.GetBytes(value));

            // Convert byte array to a string   
            var builder = new StringBuilder();

            foreach (var t in bytes)
            {
                builder.Append(t.ToString("x2"));
            }

            return builder.ToString().GetHashCode();
        }
    }
}