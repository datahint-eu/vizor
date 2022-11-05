using FluentValidation;

namespace Vizor;

public class ViInjectFluentValidator<TValidator, TModel> : ViFluentValidator<TValidator, TModel> where TValidator : IValidator<TModel>
{
	[Inject]
	private IServiceProvider ServiceProvider { get; set; } = default!;

	protected override void OnInitialized()
	{
		var tmp = ServiceProvider.GetService(typeof(TValidator));
		if (tmp is TValidator val)
			validator = val;

		if (validator == null)
			throw new InvalidOperationException($"{typeof(TValidator)} could not be created by {GetType()}");

		Context.Validator = this;
	}
}
