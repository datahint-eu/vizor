namespace Vizor;

[Flags]
public enum ButtonBehavior
{
	None = 0,

	PreventDoubleClick = 0b0001,
	ShowBusySpinner    = 0b0010
}
