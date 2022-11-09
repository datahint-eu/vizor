using System.Linq.Expressions;
using Vizor.Data;
using Vizor.Informational;

namespace Vizor.DataGrid;

public interface IViDataGridColumn
{
	bool IsVisible { get; set; }

	bool IsSortable { get; set; }
	ViSortOrder SortOrder { get; set; }

	string Name { get; set; }
}

public interface IViDataGridColumn<TItem> : IViDataGridColumn
{
	Expression<Func<TItem, object?>>? SortExpr { get; set; }

	RenderFragment? Render(TItem? item);
}