using System.Text.RegularExpressions;

namespace Vizor.Extensions;

public static class LabelExtensions
{
	private static readonly Regex pascalCaseRegex = new(@"(?<=[A-Z])(?=[A-Z][a-z]) | (?<=[^A-Z])(?=[A-Z]) | (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

	public static string ToLabel(this string? str)
	{
		if (string.IsNullOrEmpty(str))
			return string.Empty;

		var lbl = pascalCaseRegex.Replace(str.Replace("_", ""), " ");
		return char.ToUpper(lbl[0]) + lbl[1..];
	}
}
