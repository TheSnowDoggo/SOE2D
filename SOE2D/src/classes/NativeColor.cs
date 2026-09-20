using OpenTK.Mathematics;
using SDSL;

namespace SOE2D;

public class NativeColor : VariantObject
{
	private Color4 _value;

	public NativeColor(Color4 value)
	{
		_value = value;
	}
	
	public NativeColor(float r, float g, float b, float a)
	{
		_value = new Color4(r, g, b, a);
	}

	public static NativeClass Class { get; } = NativeClass.InheritObject("Color");

	public override VariantClass ObjectClass => Class;

	public Color4 Value => _value;
	
	public static Variant ToVariant(Color4 value)
	{
		return new NativeColor(value);
	}
	
	public static Color4 ToColor(Variant variant)
	{
		return variant.AsVariantObject<NativeColor>()._value;
	}

	[FunctionInfo("r", "g", "b", "a")]
	[ConstructorExport("Number", "Number", "Number", "Number", MinArgs = 3)]
	public static Variant _new(Variant[] args)
	{
		float alpha = args.Length > 3 ? args[3].AsSingle() : 1.0f;
		
		return new NativeColor(args[0].AsSingle(), args[1].AsSingle(), args[2].AsSingle(), alpha);
	}

	[PropertyExport]
	public static Variant White => new NativeColor(Color4.White);
	
	[PropertyExport]
	public static Variant Black => new NativeColor(Color4.Black);

	[PropertyExport("Number")]
	public Variant r
	{
		get => _value.R;
		set => _value.R = value.AsSingle();
	}
	
	[PropertyExport("Number")]
	public Variant g
	{
		get => _value.G;
		set => _value.G = value.AsSingle();
	}
	
	[PropertyExport("Number")]
	public Variant b
	{
		get => _value.B;
		set => _value.B = value.AsSingle();
	}
	
	[PropertyExport("Number")]
	public Variant a
	{
		get => _value.A;
		set => _value.A = value.AsSingle();
	}
	
	public override string ToStringVolatile()
	{
		return _value.ToString();
	}
	
	public override bool EqualsVolatile(VariantObject other)
	{
		return other is NativeColor v && _value == v._value;
	}
}