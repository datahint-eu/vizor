using System.Linq.Expressions;

namespace Vizor.Data;

public interface ISortableExprDataSource<TItem>
{
	Task<int> Count();

	Task<ICollection<TItem>> LoadDataAsync(int offset, int count, Expression<Func<TItem, object?>>? sortExpr, ViSortOrder sortOrder);
}