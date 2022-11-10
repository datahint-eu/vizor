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
		=> await source.LoadDataAsync(offset, count);
}
