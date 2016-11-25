using FunkyNodeIds.Bitmap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace FunkyNodeIds
{
    [Serializable]
    public class MySet : IEnumerable, ISerializable
    {
        public delegate IBitmap BitmapFactory(params int[] setbits);
        private readonly SortedDictionary<string, IBitmap> _items;
        private BitmapFactory _bitmapFactory;

        public MySet(BitmapFactory bitmapFactory)
        {
            _bitmapFactory = bitmapFactory;
            _items = new SortedDictionary<string, IBitmap>();
        }

        public void Add(Node item, bool increasing=false)
        {
            if (!_items.ContainsKey(item.Name))
            {
                _items.Add(item.Name, _bitmapFactory(item.Number));
            }
            else {
                if (increasing)
                {
                    _items[item.Name].Set(item.Number);
                }
                else
                {
                    _items[item.Name] = _items[item.Name].Or(_bitmapFactory(item.Number));
                }
            }
        }

        public bool Contains(Node item)
        {
            if (_items.ContainsKey(item.Name))
            {
                return _items[item.Name].Intersects(_bitmapFactory(item.Number));
            }
            return false;
        }
       
        public void UnionWith(MySet input)
        {
            foreach (var name in input.Cast<string>().Where(name => _items.ContainsKey(name)))
            {
                _items[name]=_items[name].Or(input._items[name]);
            }
            foreach (var name in input.Cast<string>().Where(name => !_items.ContainsKey(name)))
            {
                _items.Add(name, input._items[name]);
            }

        }

        public static MySet Merge(MySet a, MySet b, BitmapFactory bitmapFactory) {
            MySet c = new MySet(bitmapFactory);
            c.UnionWith(a);
            c.UnionWith(b);
            return c;
        }

        public void PrintSet() {
            MySet.PrintSet(this);
        }

        public static void PrintSet(MySet a)
        {
            bool first = true;
            foreach (String name in a._items.Keys)
            {
                foreach (int number in (IBitmap)a._items[name])
                {
                    if (!first)
                    {
                        Console.Write(", ");
                    }
                    first = false;
                    Console.Write("{0}/{1}", name, number);
                }
            }
            Console.WriteLine();
            long size = 0;

            using (Stream s = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(s, a);
                size = s.Length;
            }
            Console.WriteLine("memory usage: " + size + " bytes");
            Console.WriteLine("set usage: " + a.Size() + " bytes");
            Console.WriteLine();
        }

        private long Size()
        {
            long total = 0;
            foreach (string key in _items.Keys)
            {
                total += _items[key].Size();
            }
            return total;
        }


        public IEnumerator GetEnumerator()
        {
            return _items.Keys.GetEnumerator();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("data", _items, typeof(Dictionary<string, IBitmap>));
        }
    }
}
