using OpenTK.Graphics.OpenGL;

namespace SOE2D;

public sealed class TriangleMesh : Mesh2D
{
	private const int Vertices = 3;
	
	private readonly int _vbo;

	public TriangleMesh()
	{
		_vbo = GL.GenBuffer();
	}

	public unsafe void Create(
		float x1, float y1,
		float x2, float y2,
		float x3, float y3)
	{
		ThrowIfDisposed();

		const int Length = 6;

		float* data = stackalloc float[Length]
		{
			x1, y1,
			x2, y2,
			x3, y3,
		};
		
		GL.BindVertexArray(_handle);
		GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
		
		GL.BufferData(BufferTarget.ArrayBuffer, Length * sizeof(float), (nint)data, BufferUsageHint.StaticDraw);
		
		GL.VertexAttribPointer(0, VertexSize, VertexAttribPointerType.Float, false, VertexStride, 0);
		GL.EnableVertexAttribArray(0);
		
		GL.BindVertexArray(0);
		GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
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