using FluentValidation;

namespace Vizor;

public class ViInjectValidator<TValidator> : ComponentBase where TValidator : IValidator
{
	[CascadingParameter]
	private ViFormContext Context { get; set; } = default!;

	[Inject]
	private IServiceProvider ServiceProvider { get; set; } = default!;

	protected override void OnInitialized()
	{
		Context.Validator = ServiceProvider.GetService(typeof(TValidator)) as IValidator;
		//TODO: EX if validator failed to create or if Context is null
	}
}
