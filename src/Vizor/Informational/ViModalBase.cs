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

namespace Vizor.Informational;

public class ViModalBase : ComponentBase
{
	private readonly TaskCompletionSource<ModalResult> tcs = new();

	[Parameter, EditorRequired]
	public string Id { get; set; } = default!;

	[Inject]
	public ViModalService ModalService { get; set; } = default!;

	public object? Data { get; private set; }

	protected override void OnInitialized()
	{
		if (ModalService == null)
			throw new InvalidOperationException($"{typeof(ViModalService)} is not registered in the application");
	}

	protected override void OnAfterRender(bool firstRender)
	{
		if (firstRender)
		{
			ModalService.SetModalRendered(this);
		}
	}

	internal async Task<ModalResult> Show()
	{
		return await tcs.Task;
	}

	public void Close()
	{
		Data = null;
		tcs.TrySetResult(ModalResult.Close);
	}

	public void Cancel()
	{
		Data = null;
		tcs.TrySetResult(ModalResult.Cancel);
	}

	public void Submit(object? data = null)
	{
		Data = data;
		tcs.TrySetResult(ModalResult.Submit);
	}
}
