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

namespace Vizor;

public class ViFormContext
{
	private IDictionary<string, string[]>? allMessages;

	public event EventHandler<ViFormValidationEventArgs>? ValidationChanged;

	public ViFormContext(object? model)
	{
		Model = model;
	}

	public IViFormValidator? Validator { get; set; }

	public object? Model { get; }

	public bool ValidateOnSubmit() => ValidateInternal(true);

	public bool ValidateOnPropertyChange() => ValidateInternal(false);

	private bool ValidateInternal(bool submit)
	{
		var result = Validator?.Validate(out allMessages) ?? false;

		if (ValidationChanged != null)
		{
			ValidationChanged.Invoke(this, new ViFormValidationEventArgs(submit));
		}

		return result;
	}

	public bool IsValid(string? property, string? parseErrorMessage, out string cssClass, out string[]? messages)
	{
		bool retval = true;

		if (property != null && allMessages != null && allMessages.TryGetValue(property, out messages))
		{
			retval = false;
		}
		else
		{
			messages = null;
		}

		// parse error messages should almost never happen, so it's OK to use a slightly more expensive copy operation and keep the rest of the code simple
		if (parseErrorMessage != null)
		{
			if (messages?.Length > 0)
			{
				var tmp = new string[messages.Length + 1];
				Array.Copy(messages, 0, tmp, 0, messages.Length);
				messages = tmp;
			}
			else
			{
				messages = new string[1];
			}
			messages[^1] = parseErrorMessage;

			retval = false;
		}

		cssClass = retval ? "is-valid" : "is-invalid";
		return retval;
	}
}
