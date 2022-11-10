using System.Linq.Expressions;
using System.Reflection;
using Vizor.Data;
using Vizor.DataGrid.Wrappers;
using Vizor.Informational;

namespace Vizor.DataGrid;

internal class GridDataSource<TItem>
{
	private IGridDataSourceWrapper<TItem> loader;

	public GridDataSource(object? dataSource)
	{
		DataSource = dataSource;

		if (dataSource is null)
		{
			loader = new EmptyGridDataSourceWrapper<TItem>();
		}
		else if (dataSource is IReadOnlyList<TItem> list)
		{
			loader = new ListGridDataSourceWrapper<TItem>(list);
		}
		else if (dataSource is TItem[] arr)
		{
			loader = new ArrayGridDataSourceWrapper<TItem>(arr);
		}
		else if (dataSource is IDataSource<TItem> source)
		{
			loader = new NonSortableDataSourceWrapper<TItem>(source);
		}
		else if (dataSource is ISortableDataSource<TItem> sortableSource)
		{
			loader = new SortableDataSourceWrapper<TItem>(sortableSource);
		}
		else if (dataSource is ISortableExprDataSource<TItem> sortableExprSource)
		{
			loader = new SortableExprDataSourceWrapper<TItem>(sortableExprSource);
		}
		else
		{
			throw new ArgumentException($"DataSource must be of type {typeof(IReadOnlyList<TItem>)} or {typeof(IDataSource<TItem>)} or {typeof(TItem[])} or {typeof(ISortableDataSource<TItem>)} or {typeof(ISortableExprDataSource<TItem>)}");
		}
	}

	public object? DataSource { get; }

	public bool SupportsSorting => loader.SupportsSorting;

	public Task<int> Count() => loader.Count();

	public Task<ICollection<TItem>> LoadDataAsync(int offset, int count, Expression<Func<TItem, object?>>? sortExpr, ViSortOrder sortOrder)
	{
		return loader.LoadDataAsync(offset, count, sortExpr, sortOrder);
	}
}
