using System.Reflection;
using OpenTK.Graphics.OpenGL;

namespace SOE2D;

public class ShaderLoader
{
	private readonly string _baseDirectory;
	
	public ShaderLoader(string baseDirectory)
	{
		_baseDirectory = baseDirectory;
	}
	
	public string BaseDirectory => _baseDirectory;

	public Dictionary<string, Shader> LoadShaders()
	{
		var shaders = new Dictionary<string, Shader>();
		
		var directoryInfo = new DirectoryInfo(_baseDirectory);
		
		foreach (FileInfo fileInfo in directoryInfo
			         .EnumerateFiles(string.Empty, SearchOption.AllDirectories))
		{
			if (!TryShaderTypeFromExtension(fileInfo.Extension, out ShaderType shaderType))
			{
				continue;
			}
			
			string shaderSource;

			using (StreamReader sr = fileInfo.OpenText())
			{
				shaderSource = sr.ReadToEnd();
			}

			Shader shader = new Shader(shaderType);

			shader.Compile(shaderSource);

			string name = Path.GetRelativePath(directoryInfo.FullName, fileInfo.FullName)
				.Replace('\\', '/');
			
			// Should not fail as relative file paths are unique
			shaders.Add(name, shader);
		}

		return shaders;
	}

	private static bool TryShaderTypeFromExtension(string extension, out ShaderType shaderType)
	{
		switch (extension)
		{
		case ".vert":
			shaderType = ShaderType.VertexShader;
			return true;
		case ".frag":
			shaderType = ShaderType.FragmentShader;
			return true;
		default:
			shaderType = default;
			return false;
		}
	}
}