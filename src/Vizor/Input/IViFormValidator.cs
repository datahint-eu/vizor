namespace Vizor.Input;

public interface IViFormValidator
{
	bool Validate(out IDictionary<string, string[]> messages);
}
