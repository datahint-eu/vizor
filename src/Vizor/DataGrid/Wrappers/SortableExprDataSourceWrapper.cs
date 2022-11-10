using Vizor.Data;

namespace Vizor.DataGrid.Wrappers;

internal class SortableExprDataSourceWrapper<TItem> : IGridDataSourceWrapper<TItem>
{
	private readonly ISortableExprDataSource<TItem> source;

	public SortableExprDataSourceWrapper(ISortableExprDataSource<TItem> source)
	{
		this.source = source;
	}

	public bool SupportsSorting => true;

	public Task<int> Count() => source.Count();

	public async Task<ICollection<TItem>> LoadDataAsync(int offset, int count, Expression<Func<TItem, object?>>? sortExpr, ViSortOrder sortOrder)
		=> await source.LoadDataAsync(offset, count, sortExpr, sortOrder);
}
