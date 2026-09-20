using OpenTK.Graphics.OpenGL;

namespace Bad2D;

public abstract class Mesh2D : IDisposable
{
	protected const int VertexSize = 2;
	protected const int VertexStride = 2 * sizeof(float);
	
	protected readonly int _handle;

	private bool _isDisposed;

	protected Mesh2D()
	{
		_handle = GL.GenVertexArray();
	}
	
	~Mesh2D()
	{
		if (!_isDisposed)
		{
			Dispose(false);
		}
	}
	
	public int Handle
	{
		get
		{
			ThrowIfDisposed();
			return _handle;
		}
	}
	
	public abstract void Draw();

	public void Dispose()
	{
		if (_isDisposed)
		{
			return;
		}

		Dispose(true);
		GC.SuppressFinalize(this);

		_isDisposed = true;
	}

	protected virtual void Dispose(bool isDisposing)
	{
		GL.DeleteVertexArray(_handle);
	}
	
	protected void ThrowIfDisposed()
	{
		ObjectDisposedException.ThrowIf(_isDisposed, this);
	}
}