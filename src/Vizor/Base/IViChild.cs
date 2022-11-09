namespace Vizor;

public interface IViChild
{
	string Id { get; }

	RenderFragment ChildContent { get; }
}
