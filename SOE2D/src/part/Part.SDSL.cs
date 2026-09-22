using SDSL;

namespace SOE2D;

public partial class Part
{
	public static NativeClass Class { get; } = NativeClass.InheritObject("Part");

	public override VariantClass ObjectClass => Class;

	[ConstructorExport]
	public static Variant __new()
	{
		return new Part();
	}

	[PropertyExport("String")]
	public Variant __name
	{
		get => Name;
		set => Name = value.AsString();
	}
	
	[PropertyExport("Vector2")]
	public Variant __position
	{
		get => NativeVector2.ToVariant(Position);
		set => Position = NativeVector2.ToVector2(value);
	}
	
	[PropertyExport("Vector2")]
	public Variant __global_position
	{
		get => NativeVector2.ToVariant(GlobalPosition);
		set => GlobalPosition = NativeVector2.ToVector2(value);
	}
	
	[PropertyExport("Number")]
	public Variant __rotation
	{
		get => Rotation;
		set => Rotation = value.AsSingle();
	}
	
	[PropertyExport("Number")]
	public Variant __global_rotation
	{
		get => GlobalRotation;
		set => GlobalRotation = value.AsSingle();
	}
	
	[PropertyExport("Vector2")]
	public Variant __scale
	{
		get => NativeVector2.ToVariant(Scale);
		set => Scale = NativeVector2.ToVector2(value);
	}
	
	[PropertyExport("Vector2")]
	public Variant __global_scale
	{
		get => NativeVector2.ToVariant(GlobalScale);
		set => GlobalScale = NativeVector2.ToVector2(value);
	}
	
	[PropertyExport("Number")]
	public Variant __z_offset
	{
		get => ZOffset;
		set => ZOffset = value.AsSingle();
	}
	
	[PropertyExport("Color")]
	public Variant __modulate
	{
		get => NativeColor.ToVariant(Modulate);
		set => Modulate = NativeColor.ToColor(value);
	}

	[PropertyExport("Number")]
	public Variant __child_count => ChildCount;

	[PropertyExport("PackedPartView")]
	public Variant __children => new PackedPartView(_children);

	[PropertyExport("Bool")]
	public Variant __visible
	{
		get => Visible;
		set => Visible = value.AsBool();
	}

	[FunctionExport("Part")]
	public void add_child(Variant[] args)
	{
		AddChild(args[0].AsVariantObject<Part>());
	}

	[FunctionExport("Part")]
	public Variant remove_child(Variant[] args)
	{
		return RemoveChild(args[0].AsVariantObject<Part>());
	}
	
	[FunctionExport("String")]
	public Variant find_first_child(Variant[] args)
	{
		return FindFirstChild(args[0].AsString()) ?? Variant.Nil;
	}

	[FunctionExport("Number")]
	public void update() { }

	[FunctionExport("Function")]
	public static void create_task(Variant[] args)
	{
		var function = args[0].AsVariantObject<Function>();

		Task.Run(() => function.StaticInvoke());
	}
	
	[FunctionExport("Number")]
	public static void wait(Variant[] args)
	{
		double duration = args[0].AsDouble();
		
		DateTime start = DateTime.Now;
		
		while ((DateTime.Now - start).TotalSeconds < duration) { }
	}
}