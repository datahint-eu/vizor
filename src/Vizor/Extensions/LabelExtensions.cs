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
