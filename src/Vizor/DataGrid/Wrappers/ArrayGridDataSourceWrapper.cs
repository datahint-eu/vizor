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

internal class ArrayGridDataSourceWrapper<TItem> : IGridDataSourceWrapper<TItem>
{
	private TItem[] arr;

	public ArrayGridDataSourceWrapper(TItem[] arr)
	{
		this.arr = arr;
	}

	public bool SupportsSorting => true;

	public Task<int> Count() => Task.FromResult(arr.Length);

	public Task<ICollection<TItem>> LoadDataAsync(int offset, int count, Expression<Func<TItem, object?>>? sortExpr, ViSortOrder sortOrder)
	{
		if (offset >= arr.Length)
			return Task.FromResult((ICollection<TItem>)new List<TItem>(0));

		// LINQ sort
		if (sortExpr != null)
		{
			var expr = sortExpr.Compile();
			if (sortOrder == ViSortOrder.Ascending)
				arr = arr.OrderBy(expr).ToArray();
			else
				arr = arr.OrderByDescending(expr).ToArray();
		}

		// copy 'count' items to the resulting list
		var result = new List<TItem>(count);
		var index = offset;
		while (index < arr.Length && result.Count < count)
		{
			result.Add(arr[index]);
			++index;
		}

		return Task.FromResult((ICollection<TItem>)result);
	}
}
