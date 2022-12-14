using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.LinearList
{
    /// <summary>
    /// 链表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LinkedList<T>: IEnumerable<T>
    {
        /// <summary>
        /// 表长度
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// 表是否为空
        /// </summary>
        public bool IsEmpty => Count == 0;

        /// <summary>
        /// 链表的头指针
        /// </summary>
        private LinkedListNode? _head = null;

        public LinkedList()
        {
            Count = 0;
        }
        
	    /// <summary>
		/// [尾插] 添加元素
		/// </summary>
		public void Add(T item)
	    {
		    var node = new LinkedListNode
		    {
				Data = item,
				Next = null
		    };

		    if (_head is null)
		    {
			    _head = node;
			    Count++;
			    
			    return;
		    }

		    var ptr = _head;

		    while (!(ptr.Next is null))
		    {
			    ptr = ptr.Next;
		    }

		    ptr.Next = node;

		    Count++;
	    }

		/// <summary>
		/// [头插] 添加元素
		/// </summary>
		public void AddFirst(T item)
		{
			_head = new LinkedListNode
			{
				Data = item,
				Next = _head
			};

			Count++;
		}

		/// <summary>
		/// 将元素插入指定的位置
		/// </summary>
		/// <param name="item">要插入的元素</param>
		/// <param name="index">指定的位置（基于1）</param>
		public void Insert(T item, int index)
		{
			if (index < 1 || index > Count)
			{
				throw new ArgumentOutOfRangeException($"{nameof(index)}:{index} is out of range");
			}
			
			var node = new LinkedListNode
			{
				Data = item,
				Next = null
			};
			
			if (_head is null)
			{
				_head = node;
				Count++;
				
				return;
			}
			
			var ptr = _head;

			for (int i = 1; i < index - 1; i++)
			{
				ptr = ptr.Next;
			}

			node.Next = ptr.Next;
			ptr.Next = node;

			Count++;
		}

		/// <summary>
		/// 获取指定位置的元素
		/// </summary>
		/// <param name="index">指定位置（基于1）</param>
		/// <returns>指定位置的元素</returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public T GetElement(int index)
		{
			if (index < 1 || index > Count)
			{
				throw new ArgumentOutOfRangeException($"{nameof(index)}:{index} is out of range");
			}

			if (index == 1)
			{
				return _head.Data;
			}

			var ptr = _head;

			for (int i = 1; i < index; i++)
			{
				ptr = ptr.Next;
			}

			return ptr.Data;
		}

		/// <summary>
		/// 删除指定元素
		/// </summary>
		/// <param name="item">指定元素</param>
		/// <exception cref="InvalidOperationException"></exception>
		public void Remove(T item)
		{
			if (_head.Data.Equals(item))
			{
				_head = _head.Next;
				Count--;

				return;
			}
			
			var ptr = _head;

			while (!(ptr.Next is null))
			{
				if (ptr.Next.Data.Equals(item))
				{
					break;
				}

				ptr = ptr.Next;
			}

			if (ptr.Next is null)
			{
				return;
			}

			ptr.Next = ptr.Next.Next;

			Count--;
		}

		/// <summary>
		/// 删除指定位置的元素
		/// </summary>
		/// <param name="index">指定的位置（基于1）</param>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public void RemoveAt(int index)
		{
			if (index < 1 || index > Count)
			{
				throw new ArgumentOutOfRangeException($"{nameof(index)}:{index} is out of range");
			}

			if (index == 1)
			{
				_head = _head.Next;
				Count--;

				return;
			}

			var ptr = _head;

			for (int i = 1; i < index - 1; i++)
			{
				ptr = ptr.Next;
			}
			
			ptr.Next = ptr.Next.Next;

			Count--;
		}

		/// <summary>
		/// 清空表
		/// </summary>
		public void Clear()
		{
			var ptr = _head;

			while (!(ptr is null))
			{
				var node = ptr;
				ptr = ptr.Next;

				node.Data = default;
				node.Next = null;
				node = null;
			}

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
			var ptr = _head;

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
			var ptr = _head;

			while (!(ptr is null))
			{
				yield return ptr.Data;

				ptr = ptr.Next;
			}
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>
        /// 链表节点
        /// </summary>
        internal class LinkedListNode
        {
            /// <summary>
            /// 数据域
            /// </summary>
            internal T Data { get; set; }
            /// <summary>
            /// 指针域
            /// </summary>
            internal LinkedListNode? Next { get; set; }
        }
    }
}
