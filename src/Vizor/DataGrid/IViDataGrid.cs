namespace Vizor.DataGrid;

public interface IViDataGrid<TItem>
{
	void AddColumn(IViDataGridColumn<TItem> column);
}
