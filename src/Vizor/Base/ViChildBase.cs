namespace Vizor;

public class ViChildBase : ComponentBase, IViChild
{
	[CascadingParameter]
	public IViChildSelector? ChildSelector { get; set; }

	[Parameter, EditorRequired]
	public RenderFragment ChildContent { get; set; } = default!;

	protected bool IsActive => ChildSelector?.ActiveChild == this;

	protected override async Task OnInitializedAsync()
	{
		if (ChildSelector == null)
			throw new Exception(); //TODO: correct exception

		await ChildSelector.AddChild(this);
	}

	public async Task Activate()
	{
		if (ChildSelector == null)
			throw new Exception(); //TODO: correct exception

		await ChildSelector.ActivateChild(this);
	}
}
