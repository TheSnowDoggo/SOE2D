using OpenTK.Graphics.OpenGL;

namespace Bad2D;

public class ShaderProgram : IDisposable
{
	private readonly int _handle;
	private bool _isDisposed;

	public ShaderProgram()
	{
		_handle = GL.CreateProgram();
	}

	~ShaderProgram()
	{
		Dispose(false);
	}
	
	public int Handle
	{
		get
		{
			ObjectDisposedException.ThrowIf(_isDisposed, this);
			return _handle;
		}
	}
	
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (_isDisposed)
		{
			return;
		}
		
		GL.DeleteProgram(_handle);

		_isDisposed = true;
	}

	public void LinkShaders(params ReadOnlySpan<Shader> shaders)
	{
		ObjectDisposedException.ThrowIf(_isDisposed, this);
		
		// Attach shaders
		for (int i = 0; i < shaders.Length; i++)
		{
			GL.AttachShader(_handle, shaders[i].Handle);
		}
		
		GL.LinkProgram(_handle);
		
		// Detach shaders (no longer needed)
		for (int i = 0; i < shaders.Length; i++)
		{
			GL.DetachShader(_handle, shaders[i].Handle);
		}
		
		GL.GetProgram(_handle, GetProgramParameterName.LinkStatus, out int success);

		if (success != 0)
		{
			return;
		}

		string infoLog = GL.GetProgramInfoLog(_handle);

		throw new ShaderException($"Shader Program Linking Failed: {infoLog}");
	}

	public void Use()
	{
		GL.UseProgram(Handle);
	}
	
	public override string ToString()
	{
		return $"ShaderProgram<{_handle}>";
	}
}