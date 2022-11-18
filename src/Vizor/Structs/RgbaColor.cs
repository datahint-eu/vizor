namespace Vizor.Structs;

public struct RgbaColor
{
	public RgbaColor(byte r, byte g, byte b, float a, string? colorName)
	{
		R = r;
		G = g;
		B = b;
		A = a;
		ColorName = colorName;
	}

	public RgbaColor(byte r, byte g, byte b, string? colorName)
	{
		R = r;
		G = g;
		B = b;
		A = 1.0f;
		ColorName = colorName;
	}

	public byte R { get; }
	public byte G { get; }
	public byte B { get; }
	public float A { get; }

	public string? ColorName { get; }
}
