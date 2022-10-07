namespace Vizor;

public class ViTimeOnlyInput : ViInputBase<TimeOnly>
{
	public const string TimeFormat = "HH:mm:ss";

	protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out TimeOnly result, [NotNullWhen(false)] out string? validationErrorMessage)
	{
		if (TimeOnly.TryParseExact(value, TimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
		{
			validationErrorMessage = null;
			return true;
		}
		else
		{
			validationErrorMessage = $"Failed to parse '{value}' as a TimeOnly value"; //TODO: include the field name
			return false;
		}
	}
}
