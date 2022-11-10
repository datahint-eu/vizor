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

	public bool IsValid(string property, out string cssClass, out string[]? messages)
	{
		if (allMessages == null || !allMessages.TryGetValue(property, out messages))
		{
			messages = null;
			cssClass = "is-valid";
			return true;
		}

		cssClass = "is-invalid";
		return false;
	}
}
