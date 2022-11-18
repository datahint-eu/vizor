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

using Vizor.Structs;

namespace Vizor.Enums;

public struct ThemeColor
{
	public static readonly RgbaColor Primary = new(32, 107, 196, "primary");
	public static readonly RgbaColor Secondary = new(97, 104, 118, "secondary");
	public static readonly RgbaColor Success = new(47, 179, 68, "success");
	public static readonly RgbaColor Danger = new(214, 57, 57, "danger");
	public static readonly RgbaColor Warning = new(247, 103, 7, "warning");
	public static readonly RgbaColor Info = new(66, 153, 225, "info");
	public static readonly RgbaColor Light = new(248, 250, 252, "light");

	public static readonly RgbaColor Dark = new(29, 39, 59, "dark");
	public static readonly RgbaColor Muted = new(97, 104, 118, "muted");
	public static readonly RgbaColor White = new(255, 255, 255, "white");

	public static readonly RgbaColor Blue = new(32, 107, 196, "blue");
	public static readonly RgbaColor Azure = new(66, 153, 225, "azure");
	public static readonly RgbaColor Indigo = new(66, 99, 235, "indigo");
	public static readonly RgbaColor Purple = new(174, 62, 201, "purple");
	public static readonly RgbaColor Pink = new(214, 51, 108, "pink");
	public static readonly RgbaColor Red = new(214, 57, 57, "red");
	public static readonly RgbaColor Orange = new(247, 103, 7, "orange");
	public static readonly RgbaColor Yellow = new(245, 159, 0, "yellow");
	public static readonly RgbaColor Lime = new(116, 184, 22, "lime");
	public static readonly RgbaColor Green = new(47, 179, 68, "green");
	public static readonly RgbaColor Teal = new(12, 166, 120, "teal");
	public static readonly RgbaColor Cyan = new(23, 162, 184, "cyan");
}
