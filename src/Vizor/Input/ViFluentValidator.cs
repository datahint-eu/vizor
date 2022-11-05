using FluentValidation;

namespace Vizor;

public class ViFluentValidator<TValidator, TModel> : ComponentBase, IViFormValidator where TValidator : IValidator<TModel>
{
	protected TValidator? validator;

	[CascadingParameter(Name = "ViFormContext")]
	protected ViFormContext Context { get; set; } = default!;

	protected override void OnInitialized()
	{
		if (Context == null)
			throw new InvalidOperationException($"{GetType()} must be used inside a {typeof(ViForm)}");

		validator = Activator.CreateInstance<TValidator>();
		if (validator == null)
			throw new InvalidOperationException($"{typeof(TValidator)} could not be created by {GetType()}");

		Context.Validator = this;
	}

	public bool Validate(out IDictionary<string, string[]> messages)
	{
		if (validator != null && Context.Model is TModel model)
		{
			var result = validator.Validate(model);
			messages = result.ToDictionary();
			return result.IsValid;
		}

		messages = new Dictionary<string, string[]>(0);
		return false;
	}
}
