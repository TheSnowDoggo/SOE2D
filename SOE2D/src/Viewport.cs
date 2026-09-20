using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Bad2D;

public class Viewport
{
	private readonly Control _root;

	private readonly ShaderProgram _shaderProgram;
	
	private readonly int _mpLoc;
	private readonly int _modulateLoc;
	
	private readonly Stack<Control> _searchStack = [];
	
	private Vector2 _size;

	private Matrix4 _projection;

	public Viewport(Control root, ShaderProgram shaderProgram)
	{
		_root = root;
		
		_shaderProgram = shaderProgram;
		
		_mpLoc = shaderProgram.GetUniformLocation("mp");
		_modulateLoc = shaderProgram.GetUniformLocation("modulate");
	}

	public Control Root => _root;

	public Vector2 Size => _size;

	public ShaderProgram ShaderProgram => _shaderProgram;

	public void Resize(Vector2 newSize)
	{
		if (newSize == _size)
		{
			return;
		}
		
		_size = newSize;

		_projection = Matrix4.CreateOrthographicOffCenter(0, newSize.X, 0, newSize.Y, 0, 1000);
	}

	public void Draw()
	{
		if (_root == null)
		{
			return;
		}
		
		_shaderProgram.Use();

		_searchStack.Push(_root);

		while (_searchStack.TryPop(out Control parent))
		{
			if (parent.IsDrawable)
			{
				Vector2 gPosition = parent.GlobalPosition;
			
				Matrix4 model = GetModelMatrix(
					new Vector3(gPosition.X, gPosition.Y, parent.ZOffset),
					parent.GlobalRotation,
					parent.GlobalScale
				);

				Matrix4 mp = model * _projection;
				
				GL.UniformMatrix4(_mpLoc, false, ref mp);
				GL.Uniform4(_modulateLoc, parent.Modulate);
				
				parent.Draw();
			}

			if (parent.ChildCount == 0)
			{
				continue;
			}

			foreach (Control child in parent.Children)
			{
				if (!child.IsVisible)
				{
					continue;
				}
				
				_searchStack.Push(child);
			}
		}
		
		_searchStack.Clear();
	}
	
	private static Matrix4 GetModelMatrix(
		Vector3 position, float rotation, Vector2 scale)
	{
		Matrix4 model = Matrix4.Identity;

		if (scale != Vector2.One)
		{
			model *= Matrix4.CreateScale(scale.X, scale.Y, 0);
		}
		
		if (rotation != 0f)
		{
			model *= Matrix4.CreateRotationZ(rotation);
		}
		
		if (position != Vector3.Zero)
		{
			model *= Matrix4.CreateTranslation(position);
		}

		return model;
	}
}