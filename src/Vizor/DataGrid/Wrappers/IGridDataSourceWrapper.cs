using Vizor.Data;

namespace Vizor.DataGrid.Wrappers;

internal interface IGridDataSourceWrapper<TItem>
{
	bool SupportsSorting { get; }

	Task<int> Count();

	Task<ICollection<TItem>> LoadDataAsync(int offset, int count, Expression<Func<TItem, object?>>? sortExpr, ViSortOrder sortOrder);
}
