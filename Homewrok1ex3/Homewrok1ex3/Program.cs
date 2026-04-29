using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1ex3
{
    internal class MyList
    {
        private int[] _items;
        private int count;

        public MyList()
        {
            _items = new int[4];
            count = 0;
        }

        public void Add(int item)
        {
            if (count == _items.Length)
            {
                Resize();
            }

            _items[count] = item;
            count++;
        }

        public void AddRange(int[] items)
        {
            if (items == null)
            {
                return;
            }

            for (int i = 0; i < items.Length; i++)
            {
                Add(items[i]);
            }
        }

        public bool Remove(int item)
        {
            int index = -1;

            for (int i = 0; i < count; i++)
            {
                if (_items[i] == item)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                return false;
            }

            for (int i = index; i < count - 1; i++)
            {
                _items[i] = _items[i + 1];
            }

            count--;

            return true;
        }

        public bool TryGet(int index, out int value)
        {
            if (index < 0 || index >= count)
            {
                value = 0;
                return false;
            }

            value = _items[index];
            return true;
        }

        private void Resize()
        {
            int[] new_array = new int[_items.Length * 2];

            for (int i = 0; i < _items.Length; i++)
            {
                new_array[i] = _items[i];
            }

            _items = new_array;
        }

        public void Print()
        {
            for (int i = 0; i < count; i++)
            {
                Console.Write(_items[i] + " ");
            }

            Console.WriteLine();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            MyList list = new MyList();

            list.Add(10);
            list.Add(20);
            list.Add(30);

            list.Print();

            int[] arr = { 40, 50, 60 };

            list.AddRange(arr);

            list.Print();

            list.Remove(20);

            list.Print();

            int value;

            bool found = list.TryGet(2, out value);

            Console.WriteLine(found);

            if (found)
            {
                Console.WriteLine(value);
            }

            Console.ReadKey();
        }
    }
}