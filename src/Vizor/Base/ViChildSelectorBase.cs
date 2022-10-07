using Vizor.Base;

namespace Vizor;

public class ViChildSelectorBase<TChild> : ViComponentBase, IViChildSelector
{
	private readonly List<TChild> children = new();

	[Parameter, EditorRequired]
	public RenderFragment ChildContent { get; set; } = default!;

	public IViChild? ActiveChild { get; set; }

	public IEnumerable<TChild> Children => children;

	public async Task AddChild(IViChild child)
	{
		if (child is not TChild templatedChild)
			throw new ArgumentException($"argument must be of type {typeof(TChild).Name}", nameof(child));

		if (ActiveChild == null)
		{
			await ActivateChild(child);
		}
		children.Add(templatedChild);
	}

	public async Task RemoveChild(IViChild child)
	{
		if (child is not TChild templatedChild)
			throw new ArgumentException($"argument must be of type {typeof(TChild).Name}", nameof(child));

		if (ActiveChild == child)
		{
			await ActivateChild(null);
		}
		children.Remove(templatedChild);
	}

	public async Task ActivateChild(IViChild? child)
	{
		if (ActiveChild != child)
		{
			ActiveChild = child;
			await InvokeAsync(StateHasChanged);
		}
	}
}
