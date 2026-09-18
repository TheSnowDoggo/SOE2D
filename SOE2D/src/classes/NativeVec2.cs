using OpenTK.Mathematics;
using SDSL;

namespace Bad2D;

[ClassExport]
public class NativeVec2 : VariantObject
{
	private Vector2 _value;
	
	public NativeVec2(Vector2 value)
	{
		_value = value;
	}

	public NativeVec2(float x, float y)
	{
		_value = new Vector2(x, y);
	}
	
	public static NativeClass Class { get; } = NativeClass.InheritObject("Vec2");

	public override VariantClass ObjectClass => Class;

	public static void Generate(VariantAssembly assembly)
	{
		NativeClassFactory.GenerateClass<NativeVec2>(assembly, Class);
	}

	[ConstructorExport("Number", "Number")]
	public static Variant _new(Variant[] args)
	{
		return new NativeVec2(args[0].AsSingle(), args[1].AsSingle());
	}

	[PropertyExport("Number")]
	public static Variant Zero => new NativeVec2(0, 0);
	
	[PropertyExport("Number")]
	public static Variant One => new NativeVec2(1, 1);

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

	[FunctionExport("Vec2", Name = "+")]
	public Variant add(Variant[] args)
	{
		return new NativeVec2(_value + args[0].AsVariantObject<NativeVec2>()._value);
	}

	public override string ToStringVolatile()
	{
		return _value.ToString();
	}
}