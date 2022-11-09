using Vizor.Data;

namespace Vizor.DataGrid.Wrappers;

internal class NonSortableDataSourceWrapper<TItem> : IGridDataSourceWrapper<TItem>
{
	private readonly IDataSource<TItem> source;

	public NonSortableDataSourceWrapper(IDataSource<TItem> source)
	{
		this.source = source;
	}

	public bool SupportsSorting => false;

	public Task<int> Count() => source.Count();

	public async Task<ICollection<TItem>> LoadDataAsync(int offset, int count, Expression<Func<TItem, object?>>? sortExpr, ViSortOrder sortOrder)
		=> await source.Load(offset, count);
}
