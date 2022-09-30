using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.LinearList.Queue
{
    /// <summary>
    /// 链式队列
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LinkedQueue<T>: IEnumerable<T>
    {
        /// <summary>
        /// 队列长
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// 队列是否为空
        /// </summary>
        public bool IsEmpty => Count == 0;

        /// <summary>
        /// 队头指针
        /// </summary>
        private LinkedQueueNode _front = null;

        /// <summary>
        /// 队尾指针
        /// </summary>
        private LinkedQueueNode _rear = null;

        public LinkedQueue()
        {
            Count = 0;
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="elem">需要入对的元素</param>
        public void EnQueue(T elem)
        {
            var node = new LinkedQueueNode
            {
                Data = elem,
                Next = null
            };

            if (_front is null)
            {
                _front = node;
                _rear = _front;

                Count++;
                
                return;
            }

            var ptr = _front;

            while (!(ptr.Next is null))

            {
                ptr = ptr.Next;
            }

            node.Next = ptr.Next;
            ptr.Next = node;

            _rear = node;
            
            Count++;
        }

        /// <summary>
        /// 出队
        /// </summary>
        /// <returns>出队的元素</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T DeQueue()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Stack is empty");
            }

            var elem = _front.Data;
            _front = _front.Next;

            Count--;

            return elem;
        }

        /// <summary>
        /// 清空队列
        /// </summary>
        public void Clear()
        {
            while (!(_front is null))
            {
                _front = _front.Next;
            }
            
            _rear = null;
            
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
            var ptr = _front;

            while (!(ptr is null))
            {
                sb.Append($"{ptr.Data}, ");

                ptr = ptr.Next;
            }
            
            sb.Remove(sb.Length - 2, 2);

            return sb.ToString();
        }

        public IEnumerator<T> GetEnumerator()
        {
            var ptr = _front;

            while (!(ptr is null))
            {
                yield return ptr.Data;

                ptr = ptr.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        /// <summary>
        /// 链栈节点
        /// </summary>
        internal class LinkedQueueNode
        {
            /// <summary>
            /// 数据域
            /// </summary>
            internal T Data { get; set; }
            /// <summary>
            /// 指针域
            /// </summary>
            internal LinkedQueueNode? Next { get; set; }
        }
    }
}
