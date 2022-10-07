using System.Numerics;

namespace Vizor;

public class ViNumberInput<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TNumber> : ViInputBase<TNumber> where TNumber : INumber<TNumber>
{
	//TODO: TNumber? Step, Min, Max parameter

	protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out TNumber result, [NotNullWhen(false)] out string? validationErrorMessage)
	{
		//TODO: do we need explicit nullable support here ??

		var didParse = TNumber.TryParse(value, CultureInfo.CurrentCulture, out result);
		if (didParse)
		{
			validationErrorMessage = null;
		}
		else
		{
			validationErrorMessage = $"Failed to parse input '{value}' as type '{typeof(TNumber).Name}'"; //TODO: field name
		}

		return didParse;
	}
}
