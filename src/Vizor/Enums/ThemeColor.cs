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

using System.ComponentModel;
using System.Net.NetworkInformation;
using System;
using Vizor.Structs;

namespace Vizor.Enums;

public enum ThemeColor
{
	Primary,
	Secondary,
	Success,
	Danger,
	Warning,
	Info,
	Light,

	Dark,
	Muted,
	White,

	Blue,
	Azure,
	Indigo,
	Purple,
	Pink,
	Red,
	Orange,
	Yellow,
	Lime,
	Green,
	Teal,
	Cyan
}

public static class ThemeColorExtensions
{
	public static string ToCss(this ThemeColor color)
	{
		return color.ToString().ToLower();
	}

	public static RgbColor ToRgb(this ThemeColor color)
	{
		return color switch
		{
			ThemeColor.Primary => RgbColors.Primary,
			ThemeColor.Secondary => RgbColors.Secondary,
			ThemeColor.Success => RgbColors.Success,
			ThemeColor.Danger => RgbColors.Danger,
			ThemeColor.Warning => RgbColors.Warning,
			ThemeColor.Info => RgbColors.Info,
			ThemeColor.Light => RgbColors.Light,

			ThemeColor.Dark => RgbColors.Dark,
			ThemeColor.Muted => RgbColors.Muted,
			ThemeColor.White => RgbColors.White,

			ThemeColor.Blue => RgbColors.Blue,
			ThemeColor.Azure => RgbColors.Azure,
			ThemeColor.Indigo => RgbColors.Indigo,
			ThemeColor.Purple => RgbColors.Purple,
			ThemeColor.Pink => RgbColors.Pink,
			ThemeColor.Red => RgbColors.Red,
			ThemeColor.Orange => RgbColors.Orange,
			ThemeColor.Yellow => RgbColors.Yellow,
			ThemeColor.Lime => RgbColors.Lime,
			ThemeColor.Green => RgbColors.Green,
			ThemeColor.Teal => RgbColors.Teal,
			ThemeColor.Cyan => RgbColors.Cyan,

			_ => throw new ArgumentException($"ThemeColor {color} not supported")
		};
	}

	public static RgbaColor ToRgba(this ThemeColor color, float a = 1.0f)
	{
		return color.ToRgb().ToRgba(a);
	}
}