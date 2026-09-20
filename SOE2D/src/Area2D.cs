using OpenTK.Mathematics;

namespace Bad2D;

public struct Area2D
{
	private Vector2 _position;
	private Vector2 _size;

	public Area2D(Vector2 position, Vector2 size)
	{
		_position = position;
		_size = size;
	}

	public Vector2 Position
	{
		get => _position;
		set => _position = value;
	}

	public Vector2 Size
	{
		get => _size;
		set => _size = value;
	}

	public Vector2 End
	{
		get => _position + _size;
		set => _size = _position - value;
	}

	public override string ToString()
	{
		return $"[{_position}, {_size}]";
	}
}