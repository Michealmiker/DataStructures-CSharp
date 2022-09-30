using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.LinearList.Stack
{
    /// <summary>
    /// 顺序栈
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SequentialStack<T>: IEnumerable<T>
    {
        /// <summary>
        /// 容量
        /// </summary>
        public int Capacity { get; private set; }

        /// <summary>
        /// 栈长
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// 栈是否为空
        /// </summary>
        public bool IsEmpty => Count == 0;

        /// <summary>
        /// 栈内容
        /// </summary>
        private T[] _array;
        
        /// <summary>
        /// 栈底游标
        /// </summary>
        private int _baseCursor = NullCursor;
        
        /// <summary>
        /// 栈顶游标
        /// </summary>
        private int _topCursor = NullCursor;
        
        /// <summary>
        /// 栈初始化容量
        /// </summary>
        private const int StackInitSize = 1000;
        
        /// <summary>
        /// 栈增量容量
        /// </summary>
        private const int StackIncrementSize = 4;
        
        /// <summary>
        /// 栈尾游标
        /// </summary>
        private const int NullCursor = -1;
        
        public SequentialStack()
        {
            _array = new T[StackInitSize];
            
            _baseCursor = 0;
            _topCursor = _baseCursor;

            Count = 0;
            Capacity = StackInitSize;
        }

        /// <summary>
        /// 入栈
        /// </summary>
        /// <param name="elem">需要入栈的元素</param>
        /// <exception cref="ArgumentException"></exception>
        public void Push(T elem)
        {
            if (_baseCursor == _topCursor)
            {
                _array[_topCursor] = elem;

                _topCursor++;
                Count++;

                return;
            }

            if (_topCursor == Capacity)
            {
                Capacity += StackIncrementSize;

                try
                {
                    Array.Resize(ref _array, Capacity);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }

            _array[_topCursor] = elem;
            
            _topCursor++;
            Count++;
        }

        /// <summary>
        /// 出栈
        /// </summary>
        /// <returns>栈顶元素</returns>
        public T Pop()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Stack is empty");
            }
            
            --_topCursor;

            var elem = _array[_topCursor];

            Count--;
            
            return elem;
        }

        /// <summary>
        /// 获取栈顶元素
        /// </summary>
        /// <returns>栈顶元素</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Stack is empty");
            }

            return _array[_topCursor - 1];
        }

        /// <summary>
        /// 清空栈
        /// </summary>
        public void Clear()
        {
            _topCursor = _baseCursor;

            Count = 0;
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Count == 0)
            {
                return "empty";
            }

            var sb = new StringBuilder();
            var cursor = _topCursor - 1;

            while (cursor >= _baseCursor)
            {
                sb.Append($"{_array[cursor]}, ");

                cursor--;
            }
            
            sb.Remove(sb.Length - 2, 2);

            return sb.ToString();
        }

        public IEnumerator<T> GetEnumerator()
        {
            var cursor = _topCursor;

            while (cursor >= _baseCursor)
            {
                yield return _array[cursor];

                cursor--;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
