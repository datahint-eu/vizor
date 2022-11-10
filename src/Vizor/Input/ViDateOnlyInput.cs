//   Copyright 2022 DataHint BV
//   Copyright 2022 Ben Motmans
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

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
