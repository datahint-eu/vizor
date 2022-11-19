namespace Vizor.Structs;

public struct RgbaColor
{
	public RgbaColor(byte r, byte g, byte b, float a)
	{
		R = r;
		G = g;
		B = b;
		A = a;
	}

	public byte R { get; }
	public byte G { get; }
	public byte B { get; }
	public float A { get; }

	public RgbColor ToRgb() => new(R, G, B);

	public string ToCss() => $"rgba({R}, {G}, {B}, {A})";
}
