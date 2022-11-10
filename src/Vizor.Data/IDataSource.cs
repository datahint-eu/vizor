namespace Vizor.Data;

public interface IDataSource<TItem>
{
	Task<int> Count();

	Task<ICollection<TItem>> LoadDataAsync(int offset, int count);
}
