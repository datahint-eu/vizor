namespace Vizor;

public class ViDateTimeInput : ViInputBase<DateTime>
{
	//TODO: add support for DateTimeOffset and NodaTime

	public const string DateTimeFormatLocal = "yyyy-MM-ddTHH:mm:ss";

	protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out DateTime result, [NotNullWhen(false)] out string? validationErrorMessage)
	{
		if (DateTime.TryParseExact(value, DateTimeFormatLocal, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out result))
		{
			validationErrorMessage = null;
			return true;
		}
		else
		{
			validationErrorMessage = $"Failed to parse '{value}' as a DateTime value"; //TODO: include the field name
			return false;
		}
	}
}
