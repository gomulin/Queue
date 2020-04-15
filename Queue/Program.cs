using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            var myQueue = new MyQueue<int>();

            myQueue.Enqueue(1);
            myQueue.Enqueue(2);
            myQueue.Enqueue(3);
            myQueue.Enqueue(4);

            foreach (int i in myQueue.ToArray())
                Console.WriteLine(i);

            Console.WriteLine();

            while (myQueue.Count > 0)
                Console.WriteLine(myQueue.Dequeue());

            Console.ReadKey();

        }
    }

    public class MyQueue<T>
    {
        private T[] _array;
        private int _head;
        private int _tail;

        //созд очередь, емкость 0 - по умолч

        public MyQueue()

        {
            _array = new T[0];
        }

        //создаём очер на осн коллекции

        public MyQueue(IEnumerable<T> collection)

        {
            if (collection == null)
                throw new ArgumentNullException();
            _array = new T[4];
            Count = 0;
            foreach (T variable in collection)
                Enqueue(variable);

        }

        //содз оч с зад нач ёмк-тью
        //если кол-во добав-ых элтов превыс зад ёмк - увеличение

        public MyQueue(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException();
            _array = new T[capacity];
            _head = 0;
            _tail = 0;
            Count = 0;
        }

        //кол-во элтов в очереди
        public int Count { get; private set; }

        //очитска очереди
        public void Clear()
        {
            if (_head < _tail)
                Array.Clear(_array, _head, Count);
            else
            {
                Array.Clear(_array, _head, _array.Length - _head);
                Array.Clear(_array, 0, _tail);
            }
            _head = 0;
            _tail = 0;
            Count = 0;
        }

        public bool Contains(T item)
        {
            int index = _head;
            int num2 = Count;
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            while (num2-- > 0)
            {
                if (item == null)
                {
                    if (_array[index] == null)
                        return true;
                }
                else if ((_array[index] != null) && comparer.Equals(_array[index], item))
                    return true;
                index = (index + 1) % _array.Length;
            }
            return false;
        }

        public T Dequeue()
        {
            if (Count == 0)
                throw new InvalidOperationException();
            T local = _array[_head];
            _array[_head] = default(T);
            _head = (_head + 1) % _array.Length;
            Count--;
            return local;
        }

        //добавление элта в  оч

        public void Enqueue(T item)
        {
            if (Count == _array.Length)
            {
                var capacity = (int)((_array.Length * 200L) / 100L);
                if (capacity < (_array.Length + 4))
                    capacity = _array.Length + 4;
                SetCapacity(capacity);
            }
            _array[_tail] = item;
            _tail = (_tail + 1) % _array.Length;
            Count++;
        }

        //просмотр элта на верш очереди

        public T Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException();
            return _array[_head];
        }

        //изм ёмкости оч-ди

        private void SetCapacity(int capacity)
        {
            var destinationArray = new T[capacity];
            if (Count > 0)
            {
                if (_head < _tail)
                    Array.Copy(_array, _head, destinationArray, 0, Count);
                else
                {
                    Array.Copy(_array, _head, destinationArray, 0, _array.Length - _head);
                    Array.Copy(_array, 0, destinationArray, _array.Length - _head, _tail);
                }
            }
            _array = destinationArray;
            _head = 0;
            _tail = (Count == capacity) ? 0 : Count;
        }

        //преобраз очереди в массив

        public T[] ToArray()
        {
            var destinationArray = new T[Count];
            if (Count != 0)
            {
                if (_head < _tail)
                {
                    Array.Copy(_array, _head, destinationArray, 0, Count);
                    return destinationArray;
                }
                Array.Copy(_array, _head, destinationArray, 0, _array.Length - _head);
                Array.Copy(_array, 0, destinationArray, _array.Length - _head, _tail);
            }
                return destinationArray;
        }
    }
}

    
        

