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

using System.Linq.Expressions;
using Microsoft.AspNetCore.Components.Forms;
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

	protected readonly EventHandler validateChangedHandler;
	protected bool typeIsNullable;
	protected bool? isValid;
	protected string? validationProperty;
	protected string? validationCssClass;
	protected string[]? validationMessages;

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
	public EventCallback<TValue> ValueChanged { get; set; }

	[Parameter]
	public Expression<Func<TValue>>? ValidationFor { get; set; }


	//TODO: remove this param ??
	[Parameter]
	public Expression<Func<TValue>>? ValueExpression { get; set; }

	[CascadingParameter(Name = "ViFormContext")]
	private ViFormContext? Context { get; set; }


	public override async Task SetParametersAsync(ParameterView parameters)
	{
		await base.SetParametersAsync(parameters);

		if (!isInitialized)
		{
			typeIsNullable = Nullable.GetUnderlyingType(typeof(TValue)) != null;

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
			bool parseSuccess = TryParseValueFromString(value, out TValue? parsedValue, out string? validationErrorMessage);
			if (parseSuccess)
			{
				_ = SetValueAsync(parsedValue);
			}
			else if (validationErrorMessage != null)
			{
				validationMessages = new[] { validationErrorMessage };
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
				_ = await Context.ValidateAsync();
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

	private void OnValidationChanged(object? sender, EventArgs eventArgs)
	{
		if (Context is not null && validationProperty is not null)
		{
			isValid = Context.IsValid(validationProperty, out validationCssClass, out validationMessages);
		}

		StateHasChanged();
	}
}
