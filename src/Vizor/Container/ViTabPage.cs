namespace Vizor;

public class ViTabPage : ViChildBase, IViChild
{
	[Parameter, EditorRequired]
	public RenderFragment Title { get; set; } = default!;

	[Parameter]
	public RenderFragment? Icon { get; set; }

	public bool IsActiveTab => ChildSelector?.ActiveChild == this;

	protected override async Task OnInitializedAsync()
	{
		if (ChildSelector == null)
			throw new Exception(); //TODO: correct exception type

		await ChildSelector.AddChild(this);
	}
}
