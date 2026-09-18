using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Bad2D;

public class TriangleMesh : IDisposable
{
    private readonly int _handle;
    private readonly int _vbo;
    private bool _isDisposed;

    public TriangleMesh()
    {
        _handle = GL.GenVertexArray();
        _vbo = GL.GenBuffer();
    }
    
    ~TriangleMesh()
    {
        Dispose(false);
    }

    public int handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);
            return _handle;
        }
    }

    public unsafe void Create(
        float x1, float y1,
        float x2, float y2,
        float x3, float y3)
    {
        GL.BindVertexArray(_handle);
        
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);

        const int Length = 6;

        float* ptr = stackalloc float[Length]
        {
            x1, y1,
            x2, y2,
            x3, y3
        };

        GL.BufferData(BufferTarget.ArrayBuffer, Length * sizeof(float), (nint)ptr, BufferUsageHint.StaticDraw);
        
        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
        
        GL.BindVertexArray(0);
        
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Draw()
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);
        
        GL.BindVertexArray(_handle);
        
        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed)
        {
            return;
        }
		
        GL.DeleteVertexArray(_handle);
        GL.DeleteBuffer(_handle);

        _isDisposed = true;
    }
}