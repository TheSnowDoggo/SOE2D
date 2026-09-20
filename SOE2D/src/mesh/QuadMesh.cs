using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace SOE2D;

public sealed class QuadMesh : Mesh2D
{
	private const int Vertices = 6;
	
	private readonly int _vbo;

	private Vector2 _size;
	
	public QuadMesh()
	{
		_vbo = GL.GenBuffer();
	}

	public Vector2 Size => _size;

	public unsafe void Create(Vector2 size)
	{
		ThrowIfDisposed();
		
		if (size == _size)
		{
			return;
		}
		
		const int Length = 12;

		float* data = stackalloc float[Length]
		{
			// Left top triangle
			0.0f  , size.Y, // lt
			size.X, size.Y, // rt
			0.0f  , 0.0f  , // lb
			// Right bottom triangle
			size.X, size.Y, // rt
			size.X, 0.0f  , // rb
			0.0f  , 0.0f  , // lb
		};
		
		GL.BindVertexArray(_handle);
		GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
		
		GL.BufferData(BufferTarget.ArrayBuffer, Length * sizeof(float), (nint)data, BufferUsageHint.StaticDraw);

		GL.VertexAttribPointer(0, VertexSize, VertexAttribPointerType.Float, false, VertexStride, 0);
		GL.EnableVertexAttribArray(0);
		
		GL.BindVertexArray(0);
		GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
		
		_size = size;
	}

	public void Create(float width, float height)
	{
		Create(new Vector2(width, height));
	}

	public override void Draw()
	{
		ThrowIfDisposed();
		
		GL.BindVertexArray(_handle);
		GL.DrawArrays(PrimitiveType.Triangles, 0, Vertices);
	}

	protected override void Dispose(bool isDisposing)
	{
		base.Dispose(isDisposing);
		GL.DeleteBuffer(_vbo);
	}
}