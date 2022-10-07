using FluentValidation;

namespace Vizor;

public class ViFormContext
{
	public ViFormContext(object? model)
	{
		Model = model;
	}

	public IValidator? Validator { get; set; }
	public object? Model { get; }
}
