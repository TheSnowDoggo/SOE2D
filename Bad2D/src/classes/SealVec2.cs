using OpenTK.Mathematics;
using SDSL;
using SDSL.Classes;
using SDSL.Factory;
using SDSL.Prototypes;

namespace Bad2D;

[ClassExport]
public static class SealVec2
{
	public const string Vec2 = "Bad::Vec2";
	
	public static readonly SealClass Class = SealClass.CreateStruct("Bad", "Vec2", 
		static v => Unpack(v),
		static v => Unpack(v).ToString());

	public static Vector2 Unpack(this SealValue value)
	{
		return Unpack(value.AsDouble());
	}
	
	public static unsafe SealValue Pack(this Vector2 value)
	{
		return SealValue.CreateStruct(Class, *(double*)&value);
	}
	
	public static void Generate(PrototypeAssembly pAssembly)
	{
		SealClassFactory.Generate(typeof(SealVec2), pAssembly, Class);
	}

	[SealConstructor]
	[FunctionInfo("x", "y")]
	[FunctionExport(SealNumber.Number, SealNumber.Number)]
	public static SealValue _new(SealValue[] args)
	{
		return new Vector2(args[0].AsSingle(), args[1].AsSingle()).Pack();
	}
	
	[FunctionExport]
	public static SealValue x(SealValue self)
	{
		return self.Unpack().X;
	}
	
	[FunctionExport]
	public static SealValue y(SealValue self)
	{
		return self.Unpack().Y;
	}

	[FunctionExport]
	public static SealValue length(SealValue self)
	{
		return self.Unpack().Length;
	}
	
	[FunctionExport]
	public static SealValue length_squared(SealValue self)
	{
		return self.Unpack().LengthSquared;
	}
	
	[FunctionExport]
	public static SealValue floor(SealValue self)
	{
		return self.Unpack().Floor().Pack();
	}
	
	[FunctionExport]
	public static SealValue ceil(SealValue self)
	{
		return self.Unpack().Ceiling().Pack();
	}
	
	[FunctionExport]
	public static SealValue trunacte(SealValue self)
	{
		return self.Unpack().Truncate().Pack();
	}
	
	[FunctionExport]
	public static SealValue round(SealValue self)
	{
		return self.Unpack().Round().Pack();
	}
	
	[FunctionExport]
	public static SealValue abs(SealValue self)
	{
		return self.Unpack().Abs().Pack();
	}
	
	[FunctionExport]
	public static SealValue unit(SealValue self)
	{
		return self.Unpack().Normalized().Pack();
	}
	
	[FunctionInfo("other")]
	[FunctionExport(Vec2)]
	public static SealValue _multiply(SealValue self, SealValue[] args)
	{
		return (self.Unpack() * args[0].Unpack()).Pack();
	}
	
	[FunctionInfo("other")]
	[FunctionExport(Vec2)]
	public static SealValue _divide(SealValue self, SealValue[] args)
	{
		return (self.Unpack() / args[0].Unpack()).Pack();
	}
	
	[FunctionInfo("other")]
	[FunctionExport(Vec2)]
	public static SealValue _add(SealValue self, SealValue[] args)
	{
		return (self.Unpack() + args[0].Unpack()).Pack();
	}
	
	[FunctionInfo("other")]
	[FunctionExport(Vec2)]
	public static SealValue _subtract(SealValue self, SealValue[] args)
	{
		return (self.Unpack() - args[0].Unpack()).Pack();
	}

	private static unsafe Vector2 Unpack(double value)
	{
		return *(Vector2*)&value;
	}
}