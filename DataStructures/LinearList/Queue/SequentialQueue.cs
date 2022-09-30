using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.LinearList.Queue
{
    /// <summary>
    /// 顺序队列
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SequentialQueue<T>: IEnumerable<T>
    {
        /// <summary>
        /// 容量
        /// </summary>
        public int Capacity { get; private set; }

        /// <summary>
        /// 队列长
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// 队列是否为空
        /// </summary>
        public bool IsEmpty => Count == 0;
        
        /// <summary>
        /// 队列内容
        /// </summary>
        private T[] _array;

        /// <summary>
        /// 队头游标
        /// </summary>
        private int _frontCursor;
        
        /// <summary>
        /// 队尾游标
        /// </summary>
        private int _rearCursor;

        /// <summary>
        /// 队列初始化容量
        /// </summary>
        private const int QueueInitSize = 1000;
		
        /// <summary>
        /// 队列增量容量
        /// </summary>
        private const int QueueIncrementSize = 4;
        
        /// <summary>
        /// 队尾游标
        /// </summary>
        private const int NullCursor = -1;
        
        public SequentialQueue()
        {
            _array = new T[QueueInitSize];

            _frontCursor = 0;
            _rearCursor = _frontCursor;

            Count = 0;
            Capacity = QueueInitSize;
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="elem">入队元素</param>
        public void EnQueue(T elem)
        {
            if (Count == Capacity)
            {
                Capacity += QueueIncrementSize;
                
                try
                {
                    Array.Resize(ref _array, Capacity);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }

            _array[_rearCursor] = elem;

            Count++;
            _rearCursor++;
        }

        /// <summary>
        /// 出队
        /// </summary>
        /// <returns>出队元素</returns>
        public T DeQueue()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            var elem = _array[_frontCursor];

            for (int i = 0; i < Count - 1; i++)
            {
                _array[i] = _array[i + 1];
            }

            _rearCursor--;
            Count--;

            return elem;
        }

        /// <summary>
        /// 清空队列
        /// </summary>
        public void Clear()
        {
            _rearCursor = _frontCursor;

            Count--;
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Count == 0)
            {
                return "Empty";
            }
            
            var sb = new StringBuilder();
            var cursor = _frontCursor;

            while (cursor < _rearCursor)
            {
                sb.Append($"{_array[cursor]}, ");

                cursor++;
            }

            sb.Remove(sb.Length - 2, 2);

            return sb.ToString();
        }

        public IEnumerator<T> GetEnumerator()
        {
            var cursor = _frontCursor;

            while (cursor < _rearCursor)
            {
                yield return _array[cursor];

                cursor++;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
