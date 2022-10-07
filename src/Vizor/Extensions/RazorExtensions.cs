namespace Vizor.Extensions;

public static class RazorExtensions
{
	public static string? ToConditionalAttribute(this bool b) => b == true ? "true" : null;
}
