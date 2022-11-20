namespace Vizor;

public interface IViFormValidator
{
    bool Validate(out IDictionary<string, string[]> messages);
}
