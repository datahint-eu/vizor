﻿//   Copyright 2022 DataHint BV
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
