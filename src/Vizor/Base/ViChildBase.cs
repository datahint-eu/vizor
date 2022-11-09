namespace Vizor;

public class ViChildBase : ComponentBase, IViChild
{
	[CascadingParameter(Name = "IViChildSelector")]
	public IViChildSelector? ChildSelector { get; set; }

	[Parameter, EditorRequired]
	public RenderFragment ChildContent { get; set; } = default!;

	public string Id { get; } = RazorExtensions.RandomId();

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
