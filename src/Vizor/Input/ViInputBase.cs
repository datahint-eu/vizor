using System.Linq.Expressions;
using Vizor.Base;

namespace Vizor;

public abstract class ViInputBase : ViComponentBase
{
	[Parameter]
	public bool IsDisabled { get; set; }
}

public abstract class ViInputBase<TValue> : ViInputBase
{
	private bool isInitialized;
	private bool previousValueParseSuccess; //TODO: do we need this ??
	protected bool typeIsNullable;

	[Parameter]
	public bool IsReadOnly { get; set; }

	[Parameter]
	public string? Placeholder { get; set; }

	[Parameter]
	public TValue? Value { get; set; }

	[Parameter]
	public EventCallback<TValue> ValueChanged { get; set; }


	//TODO: remove this param ??
	[Parameter]
	public Expression<Func<TValue>>? ValueExpression { get; set; }

	[CascadingParameter]
	private ViFormContext? Context { get; set; }

	public ViInputBase()
	{
		AdditionalAttributes = new Dictionary<string, object>();
	}


	public override Task SetParametersAsync(ParameterView parameters)
	{
		// initialize all Parameter properties
		parameters.SetParameterProperties(this); //TODO: is this required ?

		if (!isInitialized)
		{
			typeIsNullable = Nullable.GetUnderlyingType(typeof(TValue)) != null;
			isInitialized = true;
		}

		return base.SetParametersAsync(parameters);
	}

	protected string? BindableStringValue
	{
		get => FormatValueAsString(Value);
		set
		{
			bool parseSuccess = TryParseValueFromString(value, out TValue? parsedValue, out string? validationErrorMessage);
			if (parseSuccess)
			{
				_ = SetValueAsync(parsedValue);
			}

			//TODO: validation store should set a message based on the field

			previousValueParseSuccess = parseSuccess;
		}
	}

	protected virtual string? FormatValueAsString(TValue? value)
	{
		return value?.ToString();
	}

	protected abstract bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out TValue? result, [NotNullWhen(false)] out string? validationErrorMessage);

	private async Task SetValueAsync(TValue? val)
	{
		var hasChanged = !EqualityComparer<TValue>.Default.Equals(Value, val);
		if (hasChanged)
		{
			Value = val;
			await ValueChanged.InvokeAsync(Value);

			//TODO: notify the edit context
		}
	}
}
