namespace Vizor.Extensions;

public static class NavigationExtensions
{
	// roughly based on https://github.com/dotnet/aspnetcore/blob/97e9592a60b3bafcc6091d54d8faba4e57dd6200/src/Components/Web/src/Routing/NavLink.cs
	//TODO: double check if this works with query parameters
	public static bool IsMatch(this NavigationManager navigationManager, string? absoluteLink)
	{
		if (absoluteLink == null)
			return false;

		var current = navigationManager.Uri;
		if (current == null)
			return false;

		// full match
		if (string.Equals(current, absoluteLink, StringComparison.OrdinalIgnoreCase))
		{
			return true;
		}

		// special case without trailing slash
		if (current.Length == absoluteLink.Length - 1 && absoluteLink[^1] == '/' && absoluteLink.StartsWith(current, StringComparison.OrdinalIgnoreCase))
		{
			return true;
		}

		return false;
	}
}
