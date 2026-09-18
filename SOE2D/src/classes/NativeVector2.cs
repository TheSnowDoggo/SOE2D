using OpenTK.Mathematics;
using SDSL;

namespace Bad2D;

public class NativeVector2 : VariantObject
{
	private Vector2 _value;
	
	public NativeVector2(Vector2 value)
	{
		_value = value;
	}

	public NativeVector2(float x, float y)
	{
		_value = new Vector2(x, y);
	}
	
	public static NativeClass Class { get; } = NativeClass.InheritObject("Vector2");

	public override VariantClass ObjectClass => Class;
	
	[ConstructorExport("Number", "Number")]
	public static Variant _new(Variant[] args)
	{
		return new NativeVector2(args[0].AsSingle(), args[1].AsSingle());
	}

	[PropertyExport("Number")]
	public static Variant Zero => new NativeVector2(0, 0);
	
	[PropertyExport("Number")]
	public static Variant One => new NativeVector2(1, 1);

	[PropertyExport("Number")]
	public Variant x
	{
		get => _value.X;
		set => _value.X = value.AsSingle();
	}
	
	[PropertyExport("Number")]
	public Variant y
	{
		get => _value.Y;
		set => _value.Y = value.AsSingle();
	}
	
	[PropertyExport("Number")]
	public Variant length => _value.Length;
	
	[PropertyExport("Number")]
	public Variant length_squared => _value.LengthSquared;

	[FunctionExport("Vector2", Name = "+")]
	public Variant _add(Variant[] args)
	{
		return new NativeVector2(_value + args[0].AsVariantObject<NativeVector2>()._value);
	}
	
	[FunctionExport("Vector2", Name = "-")]
	public Variant _subtract(Variant[] args)
	{
		return new NativeVector2(_value - args[0].AsVariantObject<NativeVector2>()._value);
	}
	
	[FunctionExport(Name = "u-")]
	public Variant _minus()
	{
		return new NativeVector2(-_value);
	}

	[FunctionExport]
	public Variant normalized()
	{
		return new NativeVector2(_value.Normalized());
	}
	
	[FunctionExport]
	public Variant round()
	{
		return new NativeVector2(_value.Round());
	}
	
	[FunctionExport]
	public Variant floor()
	{
		return new NativeVector2(_value.Floor());
	}
	
	[FunctionExport]
	public Variant ceil()
	{
		return new NativeVector2(_value.Ceiling());
	}
	
	[FunctionExport]
	public Variant truncate()
	{
		return new NativeVector2(_value.Truncate());
	}
	
	[FunctionExport]
	public Variant abs()
	{
		return new NativeVector2(_value.Abs());
	}

	public override string ToStringVolatile()
	{
		return _value.ToString();
	}

	public override bool EqualsVolatile(VariantObject other)
	{
		return other is NativeVector2 v && _value == v._value;
	}
}