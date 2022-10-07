using Vizor.Base;

namespace Vizor;

public class ViForm : ViComponentBase
{
	[Parameter, EditorRequired]
	public object? Model { get; set; }

	[Parameter, EditorRequired]
	public RenderFragment ChildContent { get; set; } = default!;

	public ViFormContext Context { get; set; } = default!;

	protected override Task OnInitializedAsync()
	{
		Context = new ViFormContext(Model);

		return base.OnInitializedAsync();
	}
}
