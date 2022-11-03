using System.Collections.Generic;

namespace Vizor.DataGrid;

internal class GridDataSource<TItem>
{
	private IGridDataSourceWrapper loader;

	public GridDataSource(object? dataSource)
	{
		DataSource = dataSource;

		if (dataSource is null)
		{
			ItemCount = 0;
			loader = new EmptyGridDataSourceWrapper();
		}
		else if (dataSource is IReadOnlyList<TItem> list)
		{
			ItemCount = list.Count;
			loader = new ListGridDataSourceWrapper(list);
		}
		else
		{
			throw new ArgumentException($"DataSource must be of type {nameof(IReadOnlyList<TItem>)} or ...");
		}
	}

	public int ItemCount { get; set; }
	public object? DataSource { get; }

	public Task<ICollection<TItem>> LoadDataAsync(int offset, int count)
	{
		return loader.LoadDataAsync(offset, count);
	}

	internal interface IGridDataSourceWrapper
	{
		Task<ICollection<TItem>> LoadDataAsync(int offset, int count);
	}

	internal class EmptyGridDataSourceWrapper : IGridDataSourceWrapper
	{
		public Task<ICollection<TItem>> LoadDataAsync(int offset, int count)
		{
			return Task.FromResult((ICollection<TItem>)new List<TItem>(0));
		}
	}

	internal class ListGridDataSourceWrapper : IGridDataSourceWrapper
	{
		private readonly IReadOnlyList<TItem> list;

		public ListGridDataSourceWrapper(IReadOnlyList<TItem> list)
		{
			this.list = list;
		}

		public Task<ICollection<TItem>> LoadDataAsync(int offset, int count)
		{
			if (offset >= list.Count)
				return Task.FromResult((ICollection<TItem>)new List<TItem>(0));

			var result = new List<TItem>(count);
			var index = offset;
			while (index < list.Count && result.Count < count)
			{
				result.Add(list[index]);
				++index;
			}

			return Task.FromResult((ICollection<TItem>)result);
		}
	}
}
