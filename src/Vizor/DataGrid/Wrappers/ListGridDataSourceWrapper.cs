using Vizor.Data;

namespace Vizor.DataGrid.Wrappers;

internal class ListGridDataSourceWrapper<TItem> : IGridDataSourceWrapper<TItem>
{
	private IReadOnlyList<TItem> list;

	public ListGridDataSourceWrapper(IReadOnlyList<TItem> list)
	{
		this.list = list;
	}

	public bool SupportsSorting => true;

	public Task<int> Count() => Task.FromResult(list.Count);

	public Task<ICollection<TItem>> LoadDataAsync(int offset, int count, Expression<Func<TItem, object?>>? sortExpr, ViSortOrder sortOrder)
	{
		if (offset >= list.Count)
			return Task.FromResult((ICollection<TItem>)new List<TItem>(0));

		// LINQ sort
		if (sortExpr != null)
		{
			var expr = sortExpr.Compile();
			if (sortOrder == ViSortOrder.Ascending)
				list = list.OrderBy(expr).ToList();
			else
				list = list.OrderByDescending(expr).ToList();
		}

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
