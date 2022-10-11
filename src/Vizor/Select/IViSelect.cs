namespace Vizor.Select;

public interface IViSelect<TItem>
{
	int Attach(ViSelectDataSource<TItem> dataSource);
}
