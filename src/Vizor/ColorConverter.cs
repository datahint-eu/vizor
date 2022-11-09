using Vizor.Enums;

namespace Vizor;

public static class ColorConverter
{
	public static string? ToBgColor(string? color) => color == null ? null : $"bg-{color}";

	public static string? ToLightBgColor(string? color) => color == null ? null : $"bg-{color}-lt";

	public static string? ToBadgeColor(string? color) => color == null ? null : $"badge-{color}";

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
