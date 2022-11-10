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

using Vizor.Enums;

namespace Vizor;

public static class ColorConverter
{
	public static string? ToBgColor(string? color) => color == null ? null : $"bg-{color}";

	public static string? ToLightBgColor(string? color) => color == null ? null : $"bg-{color}-lt";

	public static string? ToStatusColor(string? color) => color == null ? null : $"status-{color}";

	public static string? ToAlertColor(string? color) => color == null ? null : $"alert-{color}";

    public static string? ToNavBarColor(string? color) => color == null ? null : $"navbar-{color}";

    public static string? ToBtnColor(string? color, ButtonStyle style)
	{
		//TODO: support for pill, ghost

		return style switch
		{
			ButtonStyle.Outline => color == null ? "btn-outline" : $"btn-outline-{color}",
			_ => color == null ? null : $"btn-{color}",
		};
	}

	public static string? ToTextColor(string? color) => color == null ? null : $"text-{color}";
}
