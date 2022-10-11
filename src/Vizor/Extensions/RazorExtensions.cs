namespace Vizor.Extensions;

public static class RazorExtensions
{
	private static Random rnd = new Random();
	private const string IdCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

	public static string? ToConditionalAttribute(this bool b) => b == true ? "true" : null;

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
