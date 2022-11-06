using Microsoft.JSInterop;
using Vizor.Enums;

namespace Vizor.Informational;

public class ViModalBase : ComponentBase
{
	private readonly TaskCompletionSource<ModalResult> tcs = new();

	[Parameter, EditorRequired]
	public string Id { get; set; } = default!;

	[CascadingParameter(Name = "ViModalService")]
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
