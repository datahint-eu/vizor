namespace Vizor;

public enum ButtonType
{
	Button,
	Submit,
	Reset
}

public static class ButtonTypeExtensions
{
	public static string ToButtonType(this ButtonType type)
	{
		return type switch
		{
			ButtonType.Submit => "submit",
			ButtonType.Reset => "reset",
			_ => "button",
		};
	}
}