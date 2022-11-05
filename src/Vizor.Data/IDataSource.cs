namespace Vizor.Data;

public interface IDataSource<TItem>
{
	Task<int> Count();

	Task<ICollection<TItem>> Load(int offset, int count);
}
