using System.Reflection;
using Vizor.Data;
using Vizor.Informational;

namespace Vizor.DataGrid.Wrappers;

internal class SortableDataSourceWrapper<TItem> : IGridDataSourceWrapper<TItem>
{
	private readonly ISortableDataSource<TItem> source;

	public SortableDataSourceWrapper(ISortableDataSource<TItem> source)
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
