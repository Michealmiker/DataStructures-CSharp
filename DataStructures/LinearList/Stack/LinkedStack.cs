using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.LinearList.Stack
{
    /// <summary>
    /// 链栈
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LinkedStack<T>: IEnumerable<T>
    {
        /// <summary>
        /// 栈长
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// 表是否为空
        /// </summary>
        public bool IsEmpty => Count == 0;

        /// <summary>
        /// 栈底指针
        /// </summary>
        private LinkedStackNode _base = null;

        /// <summary>
        /// 栈顶指针
        /// </summary>
        private LinkedStackNode _top = null;

        public LinkedStack()
        {
            Count = 0;
        }

        /// <summary>
        /// 入栈
        /// </summary>
        /// <param name="elem">需要入站的元素</param>
        public void Push(T elem)
        {
            var node = new LinkedStackNode
            {
                Data = elem,
                Previous = null
            };

            if (_base is null)
            {
                _top = node;
                _base = _top;

                Count++;
                
                return;
            }

            node.Previous = _top;
            _top = node;

            Count++;
        }

        /// <summary>
        /// 出栈
        /// </summary>
        /// <returns>栈顶元素</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T Pop()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Stack is empty");
            }

            var elem = _top.Data;
            
            _top = _top.Previous;
            
            Count--;

            return elem;
        }

        /// <summary>
        /// 获取栈顶元素
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Stack is empty");
            }

            return _top.Data;
        }

        /// <summary>
        /// 清空栈
        /// </summary>
        public void Clear()
        {
            while (!(_top is null))
            {
                _top = _top.Previous;
            }
            
            _base = null;
            
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
                return "Empty";
            }

            var sb = new StringBuilder();
            var ptr = _top;

            while (!(ptr is null))
            {
                sb.Append($"{ptr.Data}, ");

                ptr = ptr.Previous;
            }
            
            sb.Remove(sb.Length - 2, 2);

            return sb.ToString();
        }

        public IEnumerator<T> GetEnumerator()
        {
            var ptr = _top;

            while (!(ptr is null))
            {
                yield return ptr.Data;

                ptr = ptr.Previous;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        /// <summary>
        /// 链栈节点
        /// </summary>
        internal class LinkedStackNode
        {
            /// <summary>
            /// 数据域
            /// </summary>
            internal T Data { get; set; }
            /// <summary>
            /// 指针域
            /// </summary>
            internal LinkedStackNode? Previous { get; set; }
        }
    }
}
