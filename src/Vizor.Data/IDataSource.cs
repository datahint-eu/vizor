namespace Vizor.Data;

public interface IDataSource<TItem>
{
	Task Load();
}
