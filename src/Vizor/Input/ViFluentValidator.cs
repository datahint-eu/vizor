using FluentValidation;

namespace Vizor;

public class ViFluentValidator<TValidator> : ComponentBase where TValidator : IValidator
{
	[CascadingParameter(Name = "ViFormContext")]
	private ViFormContext Context { get; set; } = default!;

	protected override void OnInitialized()
	{
		Context.Validator = Activator.CreateInstance<TValidator>();
		//TODO: EX if validator failed to create or if Context is null
	}
}
