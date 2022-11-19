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
using Vizor.Structs;

namespace Vizor;

public static class ColorConverter
{
	public static string? ToBgColor([NotNullIfNotNull(nameof(color))] ThemeColor? color) => color != null ? $"bg-{color.Value.ToCss()}" : null;

	public static string? ToLightBgColor([NotNullIfNotNull(nameof(color))] ThemeColor? color) => color != null ? $"bg-{color.Value.ToCss()}-lt" : null;

	public static string? ToStatusColor([NotNullIfNotNull(nameof(color))] ThemeColor? color) => color != null ? $"status-{color.Value.ToCss()}" : null;

	public static string? ToAlertColor([NotNullIfNotNull(nameof(color))] ThemeColor? color) => color != null ? $"alert-{color.Value.ToCss()}" : null;

	public static string? ToNavBarColor([NotNullIfNotNull(nameof(color))] ThemeColor? color) => color != null ? $"navbar-{color.Value.ToCss()}" : null;

	public static string? ToBtnColor([NotNullIfNotNull(nameof(color))] ThemeColor? color, ButtonStyle style)
	{
		//TODO: support for pill, ghost

		if (color != null)
		{
			return style switch
			{
				ButtonStyle.Outline => color == null ? "btn-outline" : $"btn-outline-{color.Value.ToCss()}",
				_ => color == null ? null : $"btn-{color.Value.ToCss()}",
			};
		}
		return null;
	}

	public static string? ToTextColor([NotNullIfNotNull(nameof(color))] ThemeColor? color) => color != null ? $"text-{color.Value.ToCss()}" : null;

	public static string? ToTextAndBgColor([NotNullIfNotNull(nameof(color))] ThemeColor? color) => color != null ? $"text-bg-{color.Value.ToCss()}" : null;
}
