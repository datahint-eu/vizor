namespace Vizor;

public class ViFormContext
{
	private IDictionary<string, string[]>? allMessages;

	public event EventHandler? ValidationChanged;

	public ViFormContext(object? model)
	{
		Model = model;
	}

	public IViFormValidator? Validator { get; set; }

	public object? Model { get; }

	public Task<bool> ValidateAsync()
	{
		var result = Validator?.Validate(out allMessages) ?? false;

		if (ValidationChanged != null)
		{
			ValidationChanged.Invoke(this, EventArgs.Empty);
		}

		return Task.FromResult(result);
	}

	public bool IsValid(string property, out string cssClass, out string? ariaInvalid, out string[]? messages)
	{
		if (allMessages == null || !allMessages.TryGetValue(property, out messages))
		{
			messages = null;
			cssClass = "is-valid";
			ariaInvalid = null;
			return true;
		}

		cssClass = "is-invalid";
		ariaInvalid = "true";
		return false;
	}
}
