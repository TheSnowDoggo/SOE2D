using System.Collections;
using SDSL;
using SDSL.Native;

namespace SOE2D;

public class PackedPartView : VariantObject, IReadOnlyCollection<Variant>
{
	private readonly List<Part> _parts;

	public PackedPartView(List<Part> parts)
	{
		_parts = parts;
	}
	
	public static NativeClass Class { get; } = NativeClass.InheritObject("PackedPartView");

	public override VariantClass ObjectClass => Class;

	public int Count => _parts.Count;

	[PropertyExport]
	public Variant size => _parts.Count;
	
	[FunctionExport("Number", Name = "get[]")]
	public Variant _get(Variant[] args)
	{
		return _parts[args[0].AsInt32()];
	}

	[FunctionExport]
	public Variant to_array()
	{
		return NativeArray.FromList(_parts, static v => v);
	}
	
	public override string ToStringVolatile()
	{
		return $"[ {string.Join(", ", _parts)} ]";
	}

	public IEnumerator<Variant> GetEnumerator()
	{
		for (int i = 0; i < _parts.Count; i++)
		{
			yield return _parts[i];
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}