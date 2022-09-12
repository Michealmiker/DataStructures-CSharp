using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.LinearList
{
	/// <summary>
	/// 顺序表
	/// </summary>
	public class SequentialList<T>: IEnumerable<T>
	{
		/// <summary>
		/// 容量
		/// </summary>
		public int Capacity { get; private set; }

		/// <summary>
		/// 表长
		/// </summary>
		public int Count { get; private set; }

		/// <summary>
		/// 表是否为空
		/// </summary>
		public bool IsEmpty => Count == 0;

		/// <summary>
		/// 顺序表索引器
		/// </summary>
		/// <param name="position">指定的位置（基于0）</param>
		/// <returns>指定位置的元素</returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public T this[int position]
		{
			get
			{
				// 指定的位置超过数组边界
				if (position < 0 || position >= Count)
				{
					throw new ArgumentOutOfRangeException($"{nameof(position)}:{position} is out of range");
				}

				return _array[position];
			}
			set
			{
				// 指定的位置超过数组边界
				if (position < 0 || position >= Count)
				{
					throw new ArgumentOutOfRangeException($"{nameof(position)}:{position} is out of range");
				}

				if (_array[position]!.Equals(value))
				{
					return;
				}

				_array[position] = value;
			}
		}

		/// <summary>
		/// 表内容
		/// </summary>
		private T[] _array;

		/// <summary>
		/// 表初始化容量
		/// </summary>
		private const int ListInitSize = 2;
		/// <summary>
		/// 表增量容量
		/// </summary>
		private const int ListIncrementSize = 4;

		public SequentialList()
		{
			_array = new T[ListInitSize];

			Count = 0;
			Capacity = ListInitSize;
		}

		/// <summary>
		/// [尾插] 添加元素
		/// </summary>
		public void Add(T item)
		{
			// 到达表容量
			if (Count == Capacity)
			{
				Capacity += ListIncrementSize;

				try
				{
					Array.Resize(ref _array, Capacity);
				}
				catch (ArgumentOutOfRangeException ex)
				{
					throw new ArgumentException(ex.Message);
				}
			}

			_array[Count++] = item;
		}

		/// <summary>
		/// [头插] 添加元素
		/// </summary>
		public void AddFirst(T item)
		{
			// 到达表容量
			if (Count == Capacity)
			{
				Capacity += ListIncrementSize;

				try
				{
					Array.Resize(ref _array, Capacity);
				}
				catch (ArgumentOutOfRangeException ex)
				{
					throw new ArgumentException(ex.Message);
				}
			}

			for (int i = Count - 1; i >= 0; i--)
			{
				_array[i + 1] = _array[i];
			}

			_array[0] = item;

			Count++;
		}

		/// <summary>
		/// 将元素插入指定的位置
		/// </summary>
		/// <param name="item">要插入的元素</param>
		/// <param name="index">指定的位置（基于1）</param>
		public void Insert(T item, int index)
		{
			// 指定的位置超过数组边界
			if (index < 1 || index > Count)
			{
				throw new ArgumentOutOfRangeException($"{nameof(index)}:{index} is out of range");
			}

			// 到达表容量
			if (Count == Capacity)
			{
				Capacity += ListIncrementSize;

				try
				{
					Array.Resize(ref _array, Capacity);
				}
				catch (ArgumentOutOfRangeException ex)
				{
					throw new ArgumentException(ex.Message);
				}
			}

			var pos = index - 1;

			for (int i = Count - 1; i >= pos; i--)
			{
				_array[i + 1] = _array[i];
			}

			_array[index - 1] = item;

			Count++;
		}

		/// <summary>
		/// 删除指定元素
		/// </summary>
		/// <param name="item">指定元素</param>
		/// <exception cref="InvalidOperationException"></exception>
		public void Remove(T item)
		{
			int pos = 0;

			while (pos < Count)
			{
				if (_array[pos]!.Equals(item))
				{
					break;
				}

				pos++;
			}

			// 未找到元素
			if (pos == Count)
			{
				throw new InvalidOperationException($"{item} can not find");
			}

			for (int i = pos; i < Count - 1; i++)
			{
				_array[i] = _array[i + 1];
			}

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

			for (int i = index - 1; i < Count - 1; i++)
			{
				_array[i] = _array[i + 1];
			}

			Count--;
		}

		/// <summary>
		/// 清空表
		/// </summary>
		public void Clear() => Count = 0;

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

			for (int i = 0; i < Count; i++)
			{
				sb.Append($"{_array[i]}, ");
			}

			sb.Remove(sb.Length - 2, 2);

			return sb.ToString();
		}

		public IEnumerator<T> GetEnumerator()
		{
			for (int i = 0; i < Count; i++)
			{
				yield return _array[i];
			}
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
