using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.LinearList
{
    /// <summary>
    /// 静态链表
    /// </summary>
    public class StaticLinkedList<T>: IEnumerable<T>
    {
        /// <summary>
        /// 容量
        /// </summary>
        public int Capacity { get; }
        
        /// <summary>
        /// 表长
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// 表是否为空
        /// </summary>
        public bool IsEmpty => Count == 0;

        /// <summary>
        /// 表内容
        /// </summary>
        private StaticLinkedListNode[] _array;

        /// <summary>
        /// 头节点游标
        /// </summary>
        private int _firstCursor = NullCursor;

        /// <summary>
        /// 闲置链的头节点游标
        /// </summary>
        private int _unusedFirstCursor = NullCursor;

        /// <summary>
        /// 表初始化容量
        /// </summary>
        private const int ListInitSize = 1000;
        
        /// <summary>
		/// 表尾游标
		/// </summary>
        private const int NullCursor = -1;

        public StaticLinkedList()
        {
            _array = new StaticLinkedListNode[ListInitSize];

            _firstCursor = NullCursor;
            Count = 0;
            Capacity = ListInitSize;
			
            InitUnusedNodeList();
        }

        /// <summary>
        /// 初始化闲置节点表
        /// </summary>
        private void InitUnusedNodeList()
        {
	        _unusedFirstCursor = 0;
	        
	        for (int i = 0; i < ListInitSize; i++)
	        {
		        var node = new StaticLinkedListNode
		        {
			        Data = default,
			        Cursor = NullCursor
		        };

		        if (i == 0)
		        {
			        _array[0] = node;
		        }
		        else
		        {
			        _array[i] = node;
			        _array[i - 1].Cursor = i;
		        }
	        }
        }
        
        /// <summary>
		/// [尾插] 添加元素
		/// </summary>
		public void Add(T item)
        {
	        var pos = GetNodeCursor();
	        
	        _array[pos].Data = item;
	        _array[pos].Cursor = NullCursor;
	        
	        var cursor = _firstCursor;

	        if (cursor == NullCursor)
	        {
		        _firstCursor = pos;
		        Count++;
				
		        return;
	        }

	        while (_array[cursor].Cursor != NullCursor)
	        {
		        cursor = _array[cursor].Cursor;
	        }

	        _array[cursor].Cursor = pos;

	        Count++;
        }

        /// <summary>
        /// 获取闲置节点的下标
        /// </summary>
        /// <returns>闲置节点的下标</returns>
        /// <exception cref="InvalidOperationException"></exception>
        private int GetNodeCursor()
        {
	        if (_unusedFirstCursor == NullCursor)
	        {
		        throw new InvalidOperationException("List is full");
	        }

	        var curCursor = _unusedFirstCursor;

	        _unusedFirstCursor = _array[_unusedFirstCursor].Cursor;

	        return curCursor;
        }

		/// <summary>
		/// [头插] 添加元素
		/// </summary>
		public void AddFirst(T item)
		{
			if (Count == Capacity)
			{
				throw new InvalidOperationException("List is full");
			}
			
			var pos = GetNodeCursor();
			
			_array[pos].Data = item;
			_array[pos].Cursor = NullCursor;
			
			var cursor = _firstCursor;

			if (cursor == NullCursor)
			{
				_firstCursor = pos;
				Count++;
				
				return;
			}

			_array[pos].Cursor = _firstCursor;
			_firstCursor = pos;

			Count++;
		}

		/// <summary>
		/// 将元素插入指定的位置
		/// </summary>
		/// <param name="item">要插入的元素</param>
		/// <param name="index">指定的位置（基于1）</param>
		public void Insert(T item, int index)
		{
			if (Count == Capacity)
			{
				throw new InvalidOperationException("List is full");
			}
			
			// 指定的位置超过数组边界
			if (index < 1 || index > Count)
			{
				throw new ArgumentOutOfRangeException($"{nameof(index)}:{index} is out of range");
			}
			
			var pos = GetNodeCursor();
	        
			_array[pos].Data = item;
	        
			var cursor = _firstCursor;

			for (int i = 1; i < index - 1; i++)
			{
				cursor = _array[cursor].Cursor;
			}

			_array[pos].Cursor = _array[cursor].Cursor;
			_array[cursor].Cursor = pos;

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
			// 指定的位置超过数组边界
			if (index < 1 || index > Count)
			{
				throw new ArgumentOutOfRangeException($"{nameof(index)}:{index} is out of range");
			}

			if (index == 1)
			{
				return _array[_firstCursor].Data;
			}
			
			var cursor = _firstCursor;

			for (int i = 1; i < index; i++)
			{
				cursor = _array[cursor].Cursor;
			}
			
			return _array[cursor].Data;
		}

		/// <summary>
		/// 删除指定元素
		/// </summary>
		/// <param name="item">指定元素</param>
		/// <exception cref="InvalidOperationException"></exception>
		public void Remove(T item)
		{
			var tmpPos = 0;
			
			if (_array[_firstCursor].Data.Equals(item))
			{
				tmpPos = _firstCursor;
				
				_firstCursor = _array[_firstCursor].Cursor;
				_array[tmpPos].Cursor = _unusedFirstCursor;
				_unusedFirstCursor = tmpPos;

				Count--;

				return;
			}
			
			var cursor = _firstCursor;

			while (_array[cursor].Cursor != NullCursor)
			{
				if (_array[_array[cursor].Cursor].Data.Equals(item))
				{
					break;
				}

				cursor = _array[cursor].Cursor;
			}

			if (cursor == NullCursor)
			{
				throw new InvalidOperationException($"{item} can not find");
			}

			tmpPos = _array[cursor].Cursor;
			_array[cursor].Cursor = _array[_array[cursor].Cursor].Cursor;
			_array[tmpPos].Cursor = _unusedFirstCursor;
			_unusedFirstCursor = tmpPos;

			Count--;
		}

		/// <summary>
		/// 删除指定位置的元素
		/// </summary>
		/// <param name="index">指定的位置（基于1）</param>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public void RemoveAt(int index)
		{
			// 指定的位置超过数组边界
			if (index < 1 || index > Count)
			{
				throw new ArgumentOutOfRangeException($"{nameof(index)}:{index} is out of range");
			}
			
			var tmpPos = 0;
			
			if (index == 1)
			{
				tmpPos = _firstCursor;
				
				_firstCursor = _array[_firstCursor].Cursor;
				_array[tmpPos].Cursor = _unusedFirstCursor;
				_unusedFirstCursor = tmpPos;

				Count--;

				return;
			}

			var cursor = _firstCursor;

			for (int i = 1; i < index - 1; i++)
			{
				cursor = _array[cursor].Cursor;
			}
			
			tmpPos = _array[cursor].Cursor;
			_array[cursor].Cursor = _array[_array[cursor].Cursor].Cursor;
			_array[tmpPos].Cursor = _unusedFirstCursor;
			_unusedFirstCursor = tmpPos;

			Count--;
		}

		/// <summary>
		/// 清空表
		/// </summary>
		public void Clear()
		{
			for (int i = 0; i < Count; i++)
			{
				var tmpPos = _firstCursor;
				
				_firstCursor = _array[_firstCursor].Cursor;
				_array[tmpPos].Cursor = _unusedFirstCursor;
				_unusedFirstCursor = tmpPos;
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
			var cursor = _firstCursor;

			while (cursor != NullCursor)
			{
				sb.Append($"{_array[cursor].Data}, ");

				cursor = _array[cursor].Cursor;
			}
			
			sb.Remove(sb.Length - 2, 2);
			
			return sb.ToString();
		}

		public IEnumerator<T> GetEnumerator()
		{
			var cursor = _firstCursor;

			while (cursor != NullCursor)
			{
				yield return _array[cursor].Data;

				cursor = _array[cursor].Cursor;
			}
		}
		
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>
		/// 静态链表节点
		/// </summary>
		internal class StaticLinkedListNode
		{
			/// <summary>
			/// 数据
			/// </summary>
			internal T Data { get; set; }
			/// <summary>
			/// 游标
			/// </summary>
			internal int Cursor { get; set; }
		}
    }
}
