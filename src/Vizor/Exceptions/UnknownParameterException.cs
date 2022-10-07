namespace Vizor.Exceptions;

public class UnknownParameterException : ArgumentException
{
	public UnknownParameterException(string paramName, Type componentType)
		:	base($"Unknown parameter '{paramName}' in component '{componentType.Name}'")
	{
	}
}
