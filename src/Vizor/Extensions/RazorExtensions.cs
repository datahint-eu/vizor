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

namespace Vizor.Extensions;

public static class RazorExtensions
{
	private static Random rnd = new Random();
	private const string IdCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

	public static string? ToConditionalAttribute(this bool b) => b == true ? "true" : null;

	public static string? ToConditionalAttributeInverse(this bool? b) => b == false ? "true" : null;

	public static string RandomId(int len = 8)
	{
		var result = new char[len];

		for (int i = 0; i < len; ++i)
		{
			result[i] = IdCharacters[rnd.Next(IdCharacters.Length)];
		}

		return "id_" + new string(result);
	}
}
