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

public class ViChildSelectorBase<TChild> : ViComponentBase, IViChildSelector where TChild : ViChildBase
{
	private readonly List<TChild> children = new();

	[Parameter, EditorRequired]
	public RenderFragment ChildContent { get; set; } = default!;

	public IViChild? ActiveChild { get; set; }

	public IEnumerable<TChild> Children => children;

	public async Task AddChild(IViChild child)
	{
		if (child is not TChild templatedChild)
			throw new ArgumentException($"argument must be of type {typeof(TChild).Name}", nameof(child));

		if (ActiveChild == null)
		{
			await ActivateChild(child);
		}
		children.Add(templatedChild);
	}

	public async Task RemoveChild(IViChild child)
	{
		if (child is not TChild templatedChild)
			throw new ArgumentException($"argument must be of type {typeof(TChild).Name}", nameof(child));

		if (ActiveChild == child)
		{
			await ActivateChild(null);
		}
		children.Remove(templatedChild);
	}

	public async Task ActivateChild(IViChild? child)
	{
		if (ActiveChild != child)
		{
			ActiveChild = child;
			await InvokeAsync(StateHasChanged);
		}
	}
}
