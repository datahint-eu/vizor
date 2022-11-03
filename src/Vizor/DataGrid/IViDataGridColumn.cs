namespace Vizor.DataGrid;

public interface IViDataGridColumn
{
	bool IsVisible { get; set; }

	bool IsSortable { get; set; }

	string Name { get; set; }
}

public interface IViDataGridColumn<TItem> : IViDataGridColumn
{
	RenderFragment? Render(TItem? item);
}