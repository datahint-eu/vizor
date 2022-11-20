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

using System.Reflection;
using Vizor.Data;

namespace Vizor.DataGrid.Wrappers;

internal class SortableDataSourceWrapper<TItem> : IGridDataSourceWrapper<TItem>
{
	private readonly ISortableGridDataSource<TItem> source;

	public SortableDataSourceWrapper(ISortableGridDataSource<TItem> source)
	{
		this.source = source;
	}

	public bool SupportsSorting => true;

	public Task<int> Count() => source.Count();

	public async Task<ICollection<TItem>> LoadDataAsync(int offset, int count, Expression<Func<TItem, object?>>? sortExpr, ViSortOrder sortOrder)
	{
		string? memberName = null;

		if (sortExpr != null)
		{
			// see https://stackoverflow.com/a/672212/51650
			var member = sortExpr.Body as MemberExpression;
			if (member == null)
			{
				var unary = sortExpr.Body as UnaryExpression;
				member = unary?.Operand as MemberExpression;

				if (member == null)
				{
					throw new ArgumentException(string.Format("Expression '{0}' refers to a method, not a property.", sortExpr.ToString()));
				}
			}


			if (member.Member is PropertyInfo propInfo)
			{
				memberName = propInfo.Name;
			}
			else if (member.Member is FieldInfo fieldInfo)
			{
				memberName = fieldInfo.Name;
			}
			else
			{
				throw new ArgumentException(string.Format("Expression '{0}' does not refer to a field or property.", sortExpr.ToString()));
			}
		}

		return await source.LoadDataAsync(offset, count, memberName, sortOrder);
	}
}
