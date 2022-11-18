using System.Numerics;

namespace Vizor.Data.Structs;

public struct ChartValue<TLabel, TValue> where TValue : INumber<TValue>
{
	public ChartValue(TLabel label, TValue value)
	{
		Label = label;
		Value = value;
	}

	public TLabel Label { get; set; }

	public TValue Value { get; set; }
}
