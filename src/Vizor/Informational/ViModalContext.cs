using System.Reflection;
using Microsoft.JSInterop;
using Vizor.Enums;

namespace Vizor.Informational;

public class ViModalContext : IDisposable
{
	private readonly TaskCompletionSource<ModalResult> tcs = new();
	private readonly ViModalService modalService;
	private readonly Type modalType;

	public ViModalContext(ViModalService modalService, Type modalType, string modalId)
	{
		this.modalService = modalService;
		this.modalType = modalType;
		ModalId = modalId;
		ToggleId = modalId + "-toggle";

		ModalFragment = RenderModal();
		ToggleFragment = RenderToggle();
	}

	public RenderFragment ModalFragment { get; }

	public RenderFragment ToggleFragment { get; }

	public object? Data { get; private set; }
	public string ModalId { get; }

	public string ToggleId { get; }

	internal bool IsRendered { get; set; }

	/*
	public async Task<ModalResult> Show()
	{
		await modalService.Show(this);

		var result = await tcs.Task;
		return result;
	}
	*/

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

	public void Submit(object? data)
	{
		Data = data;
		tcs.TrySetResult(ModalResult.Submit);
	}

	private RenderFragment RenderModal() => builder =>
	{
		builder.OpenComponent(0, modalType);
		builder.AddAttribute(1, "Id", ModalId);
		//TODO: @key attribute ?
		builder.CloseComponent();
	};

	private RenderFragment RenderToggle() => builder =>
	{
		builder.OpenElement(0, "a");
		builder.AddAttribute(1, "data-bs-toggle", "modal");
		builder.AddAttribute(2, "data-bs-target", "#" + ModalId);
		builder.AddAttribute(3, "class", "invisible");
		builder.AddAttribute(4, "id", ToggleId);
		builder.AddAttribute(5, "aria-hidden", "true");
		builder.CloseElement();
	};

	public void Dispose()
	{
		/*
		modalService.Destroy(this);
		*/
	}
}
