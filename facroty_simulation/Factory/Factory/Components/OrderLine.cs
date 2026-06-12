using Factory.Models;

namespace Factory.Components
{
    public class OrderLine
    {
        private class Node
        {
            public Item Value { get; }
            public Node Next { get; set; }

            public Node(Item value)
            {
                Value = value;
                Next = null;
            }
        }

        private Node _head;   
        private Node _tail;   
        private int _count;
        private readonly int _capacity;

        public int Count => _count;
        public bool IsEmpty => _count == 0;
        public bool IsFull => _count >= _capacity;
        public int Capacity => _capacity;

        public OrderLine(int capacity)
        {
            _capacity = capacity;
            _head = null;
            _tail = null;
            _count = 0;
        }

        
        public bool TryEnqueue(Item item)
        {
            if (IsFull)
                return false;

            Node newNode = new Node(item);

            if (_tail == null)
            {
                // Queue was empty new node is both head and tail
                _head = newNode;
                _tail = newNode;
            }
            else
            {
                // Attach new node after current tail, then move tail forward
                _tail.Next = newNode;
                _tail = newNode;
            }

            _count++;
            return true;
        }

        public bool TryDequeue(out Item item)
        {
            if (IsEmpty)
            {
                item = null;
                return false;
            }

            item = _head.Value;
            _head = _head.Next;

            if (_head == null)
            {
                // Queue is now empty tail must also be null
                _tail = null;
            }

            _count--;
            return true;
        }

        public bool TryPeek(out Item item)
        {
            if (IsEmpty)
            {
                item = null;
                return false;
            }

            item = _head.Value;
            return true;
        }

        public override string ToString()
        {
            return $"OrderLine[{_count}/{_capacity}]";
        }
    }
}