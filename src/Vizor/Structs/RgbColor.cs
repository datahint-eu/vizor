namespace Vizor.Structs;

public struct RgbColor
{

	public RgbColor(byte r, byte g, byte b)
	{
		R = r;
		G = g;
		B = b;
	}

	public byte R { get; }
	public byte G { get; }
	public byte B { get; }

	public RgbaColor ToRgba(float a = 1.0f) => new(R, G, B, a);

	public string ToCss() => $"rgb({R}, {G}, {B})";
}
