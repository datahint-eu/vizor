namespace Vizor
{
	public interface IViChildSelector
	{
		IViChild? ActiveChild { get; set; }

		Task ActivateChild(IViChild? child);

		Task AddChild(IViChild child);
	}
}