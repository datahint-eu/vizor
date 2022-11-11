//   Copyright 2022 DataHint BV
//   Copyright 2022 Ben Motmans
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

using Vizor.Base;

namespace Vizor;

public abstract class ViInputBase : ViComponentBase
{
	[Parameter]
	public bool IsDisabled { get; set; }
}

public abstract class ViInputBase<TValue> : ViInputBase, IDisposable
{
	private bool isInitialized;

	protected readonly EventHandler<ViFormValidationEventArgs> validateChangedHandler;
	protected bool typeIsNullable;
	protected bool valueChangedOnce; // keep track if the value changed at least once, the 
	protected bool? isValid;
	protected string? validationProperty;
	protected string? validationCssClass;
	protected string[]? validationMessages;
	protected string? parseErrorMessage;

	public ViInputBase()
	{
		//TODO: why did I do this ?
		AdditionalAttributes = new Dictionary<string, object>();

		validateChangedHandler = OnValidationChanged;
	}

	[Parameter]
	public bool IsReadOnly { get; set; }

	[Parameter]
	public string? Placeholder { get; set; }

	[Parameter]
	public TValue? Value { get; set; }

	[Parameter]
	public EventCallback<TValue?> ValueChanged { get; set; }

	[Parameter]
	public Expression<Func<TValue?>>? ValidationFor { get; set; }

	[CascadingParameter(Name = "ViFormContext")]
	private ViFormContext? Context { get; set; }


	public override async Task SetParametersAsync(ParameterView parameters)
	{
		await base.SetParametersAsync(parameters);

		if (!isInitialized)
		{
			typeIsNullable = Nullable.GetUnderlyingType(typeof(TValue)) != null; // used in base classes

			validationProperty = ValidationFor?.ToPropertyChain();

			if (Context is not null)
			{
				Context.ValidationChanged += validateChangedHandler;
			}

			isInitialized = true;
		}
	}

	protected string? BindableStringValue
	{
		get => FormatValueAsString(Value);
		set
		{
			valueChangedOnce = true;

			bool parseSuccess = TryParseValueFromString(value, out TValue? parsedValue, out string? validationErrorMessage);
			if (parseSuccess)
			{
				parseErrorMessage = null;
				_ = SetValueAsync(parsedValue);
			}
			else if (validationErrorMessage != null)
			{
				parseErrorMessage = validationErrorMessage;
				Context?.ValidateOnPropertyChange();
			}
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

			if (Context is not null && validationProperty is not null)
			{
				_ = Context.ValidateOnPropertyChange();
			}
		}
	}

	protected RenderFragment? RenderValidationMessages() => builder =>
	{
		if (validationMessages == null)
			return;

		int sequence = -1;
		foreach (var msg in validationMessages)
		{
			// <div class="invalid-feedback">Invalid feedback</div>

			builder.OpenElement(++sequence, "div");
			builder.AddAttribute(++sequence, "class", "invalid-feedback");
			builder.AddContent(++sequence, msg);
			builder.CloseElement();
		}
	};

	public void Dispose()
	{
		if (Context is not null)
		{
			Context.ValidationChanged -= validateChangedHandler;
		}
	}

	private void OnValidationChanged(object? sender, ViFormValidationEventArgs e)
	{
		// this prevents the visual validation state to change if we never even tried to change the property value
		// UNLESS the form was submitted
		if (!e.Submit && !valueChangedOnce)
			return;

		if (Context is not null)
		{
			isValid = Context.IsValid(validationProperty, parseErrorMessage, out validationCssClass, out validationMessages);
		}

		StateHasChanged();
	}
}
