//   Copyright 2022 DataHint BV
//   Copyright 2022 Ben Motmans
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

using Vizor.DataGrid.Wrappers;
using Vizor.Data;

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
		else if (dataSource is IGridDataSource<TItem> source)
		{
			loader = new NonSortableDataSourceWrapper<TItem>(source);
		}
		else if (dataSource is ISortableGridDataSource<TItem> sortableSource)
		{
			loader = new SortableDataSourceWrapper<TItem>(sortableSource);
		}
		else if (dataSource is ISortableExprGridDataSource<TItem> sortableExprSource)
		{
			loader = new SortableExprDataSourceWrapper<TItem>(sortableExprSource);
		}
		else
		{
			throw new ArgumentException($"DataSource must be of type {typeof(IReadOnlyList<TItem>)} or {typeof(IGridDataSource<TItem>)} or {typeof(TItem[])} or {typeof(ISortableGridDataSource<TItem>)} or {typeof(ISortableExprGridDataSource<TItem>)}");
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
