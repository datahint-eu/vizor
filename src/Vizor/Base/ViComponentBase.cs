namespace Vizor.Base;

public abstract class ViComponentBase : ComponentBase
{
    [Parameter]
    public string? CssClass { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> AdditionalAttributes { get; set; } = default!;
}
