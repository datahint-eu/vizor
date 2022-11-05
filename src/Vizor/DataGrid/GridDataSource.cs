using Vizor.Data;

namespace Vizor.DataGrid;

internal class GridDataSource<TItem>
{
	private IGridDataSourceWrapper loader;

	public GridDataSource(object? dataSource)
	{
		DataSource = dataSource;

		if (dataSource is null)
		{
			loader = new EmptyGridDataSourceWrapper();
		}
		else if (dataSource is IReadOnlyList<TItem> list)
		{
			loader = new ListGridDataSourceWrapper(list);
		}
		else if (dataSource is IDataSource<TItem> source)
		{
			loader = new GridDataSourceWrapper(source);
		}
		else
		{
			throw new ArgumentException($"DataSource must be of type {nameof(IReadOnlyList<TItem>)} or ...");
		}
	}

	public object? DataSource { get; }

	public Task<int> Count() => loader.Count();

	public Task<ICollection<TItem>> LoadDataAsync(int offset, int count)
	{
		return loader.LoadDataAsync(offset, count);
	}

	internal interface IGridDataSourceWrapper
	{
		Task<int> Count();

		Task<ICollection<TItem>> LoadDataAsync(int offset, int count);
	}

	internal class EmptyGridDataSourceWrapper : IGridDataSourceWrapper
	{
		public Task<int> Count() => Task.FromResult(0);

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

		public Task<int> Count() => Task.FromResult(list.Count);

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

	internal class GridDataSourceWrapper : IGridDataSourceWrapper
	{
		private readonly IDataSource<TItem> source;

		public GridDataSourceWrapper(IDataSource<TItem> source)
		{
			this.source = source;
		}

		public Task<int> Count() => source.Count();

		public async Task<ICollection<TItem>> LoadDataAsync(int offset, int count)
			=> await source.Load(offset, count);
	}
}
