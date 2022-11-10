using System.Linq.Expressions;

namespace Vizor.Data;

public interface ISortableDataSource<TItem>
{
	Task<int> Count();

	Task<ICollection<TItem>> LoadDataAsync(int offset, int count, string? propertyName, ViSortOrder sortOrder);
}