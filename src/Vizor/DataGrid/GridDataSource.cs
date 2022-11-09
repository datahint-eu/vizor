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
		else
		{
			//TODO: all types
			throw new ArgumentException($"DataSource must be of type {typeof(IReadOnlyList<TItem>)} or {typeof(IDataSource<TItem>)} or {typeof(TItem[])} or ...");
		}

		//TODO: sortable data source --> SupportsSorting = true
		//TODO: if List or arr[] --> reflection based sorting
	}

	public object? DataSource { get; }

	public bool SupportsSorting => loader.SupportsSorting;

	public Task<int> Count() => loader.Count();

	public Task<ICollection<TItem>> LoadDataAsync(int offset, int count, Expression<Func<TItem, object?>>? sortExpr, ViSortOrder sortOrder)
	{
		return loader.LoadDataAsync(offset, count, sortExpr, sortOrder);
	}
}
