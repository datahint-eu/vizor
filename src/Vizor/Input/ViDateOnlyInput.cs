namespace Vizor;

public class ViDateOnlyInput : ViInputBase<DateOnly>
{
	public const string DateFormat = "yyyy-MM-dd";

	protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out DateOnly result, [NotNullWhen(false)] out string? validationErrorMessage)
	{
		if (DateOnly.TryParseExact(value, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
		{
			validationErrorMessage = null;
			return true;
		}
		else
		{
			validationErrorMessage = $"Failed to parse '{value}' as a DateOnly value"; //TODO: include the field name
			return false;
		}
	}
}
