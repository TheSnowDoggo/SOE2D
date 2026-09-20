using OpenTK.Graphics.OpenGL;

namespace SOE2D;

public class Shader : IDisposable
{
	private readonly int _handle;
	private readonly ShaderType _shaderType;
	private bool _isDisposed;

	public Shader(ShaderType shaderType)
	{
		_handle = GL.CreateShader(shaderType);
		_shaderType = shaderType;
	}

	public int Handle
	{
		get
		{
			ObjectDisposedException.ThrowIf(_isDisposed, this);
			return _handle;
		}
	}
	
	public ShaderType ShaderType => _shaderType;

	~Shader()
	{
		Dipose(false);
	}

	public void Dispose()
	{
		Dipose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dipose(bool disposing)
	{
		if (_isDisposed)
		{
			return;
		}
		
		GL.DeleteShader(_handle);

		_isDisposed = true;
	}

	public void Compile(string shaderSource)
	{
		ObjectDisposedException.ThrowIf(_isDisposed, this);
		
		GL.ShaderSource(_handle, shaderSource);
		
		GL.CompileShader(_handle);
		
		GL.GetShader(_handle, ShaderParameter.CompileStatus, out int success);

		if (success != 0)
		{
			return;
		}

		string infoLog = GL.GetShaderInfoLog(_handle);
		
		throw new ShaderException($"Shader Compilation Failed: {infoLog}");
	}

	public override string ToString()
	{
		return $"{_shaderType}<{_shaderType}, {_handle}>";
	}
}