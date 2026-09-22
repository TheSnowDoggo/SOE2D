using OpenTK.Mathematics;
using SDSL;

namespace SOE2D;

public partial class Part : VariantObject
{
	private readonly List<Part> _children = [];
	
	private Part _parent;

	private Vector2 _position;
	private Vector2 _globalPosition;

	private float _rotation;
	private float _globalRotation;

	private Vector2 _scale       = Vector2.One;
	private Vector2 _globalScale = Vector2.One;

	public string Name { get; set; }

	public Part Parent => _parent;
	
	public Vector2 Position
	{
		get => _position;
		set
		{
			if (value == _position)
			{
				return;
			}

			_position = value;

			if (_parent == null)
			{
				_globalPosition = value;
			}
			else
			{
				_globalPosition = _parent._globalPosition + value;
				UpdateChildrenPositions();
			}
		}
	}
	
	public Vector2 GlobalPosition
	{
		get => _globalPosition;
		// Call position setter to update child positions
		set => Position += value - _globalPosition;
	}
	
	public float Rotation
	{
		get => _rotation;
		set
		{
			if (value == _rotation)
			{
				return;
			}

			_rotation = value;

			if (_parent == null)
			{
				_globalRotation = value;
			}
			else
			{
				_globalRotation = _parent._globalRotation + value;
				UpdateChildrenRotations();
			}
		}
	}

	public float GlobalRotation
	{
		get => _globalRotation;
		// Call rotation setter to update child rotations
		set => Rotation += value - _globalRotation;
	}

	public Vector2 Scale
	{
		get => _scale;
		set
		{
			if (value == _scale)
			{
				return;
			}

			_scale = value;
			
			if (_parent == null)
			{
				_globalScale = value;
			}
			else
			{
				_globalScale = _parent._globalScale + value;
				UpdateChildrenScales();
			}
		}
	}

	public Vector2 GlobalScale
	{
		get => _globalScale;
		// Call scale setter to update child scales
		set => Scale += value - _globalScale;
	}
	
	public float ZOffset { get; set; }

	public Color4 Modulate { get; set; } = Color4.White;
	
	public bool Visible { get; set; } = true;

	public virtual bool Drawable => false;
	
	public int ChildCount => _children.Count;
	 
	public IReadOnlyList<Part> Children => _children;
	
	public void AddChild(Part child)
	{
		if (child._parent != null)
		{
			throw new InvalidOperationException("Child already has a parent.");
		}
		
		_children.Add(child);
		
		child._parent = this;

		// Update transform
		child._globalPosition = _globalPosition + child._position;
		child.UpdateChildrenPositions();

		child._globalRotation = _globalRotation + child._rotation;
		child.UpdateChildrenRotations();

		child._globalScale = _globalScale + child._scale;
		child.UpdateChildrenScales();
	}
	
	public bool RemoveChild(int index)
	{
		if (index < 0 || index >= _children.Count)
		{
			return false;
		}

		Part child = _children[index];
		
		_children.RemoveAt(index);
		
		child._parent = null;
		
		// Clear transform
		child._globalPosition = _position;
		child.UpdateChildrenPositions();

		child._globalRotation = _rotation;
		child.UpdateChildrenRotations();

		child._globalScale = _scale;
		child.UpdateChildrenScales();

		return true;
	}
	
	public bool RemoveChild(Part child)
	{
		return RemoveChild(_children.IndexOf(child));
	}
	
	public Part GetChild(int index)
	{
		return _children[index];
	}
	
	public Part FindFirstChild(string name, StringComparison comparison = StringComparison.Ordinal)
	{
		for (int i = 0; i < _children.Count; i++)
		{
			Part child = _children[i];
			
			if (child.Name.Equals(name, comparison))
			{
				return child;
			}
		}

		return null;
	}

	public int FindFirstChildIndex(string name, StringComparison comparison = StringComparison.Ordinal)
	{
		for (int i = 0; i < _children.Count; i++)
		{
			if (_children[i].Name.Equals(name, comparison))
			{
				return i;
			}
		}

		return -1;
	}

	public virtual void Draw() { }

	public override string ToString()
	{
		return $"{GetType().Name}<{Name}>";
	}

	private void UpdateChildrenPositions()
	{
		for (int i = 0; i < _children.Count; i++)
		{
			Part child = _children[i];

			child._globalPosition = _globalPosition + child._position;
			child.UpdateChildrenPositions();
		}
	}
	
	private void UpdateChildrenRotations()
	{
		for (int i = 0; i < _children.Count; i++)
		{
			Part child = _children[i];

			child._globalRotation = _globalRotation + child._rotation;
			child.UpdateChildrenRotations();
		}
	}
	
	private void UpdateChildrenScales()
	{
		for (int i = 0; i < _children.Count; i++)
		{
			Part child = _children[i];

			child._globalScale = _globalScale + child._scale;
			child.UpdateChildrenScales();
		}
	}
}