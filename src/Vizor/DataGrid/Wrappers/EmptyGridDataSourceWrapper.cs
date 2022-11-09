using Vizor.Data;

namespace Vizor.DataGrid.Wrappers;

internal class EmptyGridDataSourceWrapper<TItem> : IGridDataSourceWrapper<TItem>
{
	public Task<int> Count() => Task.FromResult(0);

	public bool SupportsSorting => false;

	public Task<ICollection<TItem>> LoadDataAsync(int offset, int count, Expression<Func<TItem, object?>>? sortExpr, ViSortOrder sortOrder)
	{
		return Task.FromResult((ICollection<TItem>)new List<TItem>(0));
	}
}
