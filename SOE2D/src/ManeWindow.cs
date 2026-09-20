using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace SOE2D;

public class ManeWindow : GameWindow
{
	private const string ResourceDirectory = @"C:\Users\redst\RiderProjects\Bad2D\SOE2D\res";
	
	private Viewport _viewport;
	
	private Dictionary<string, Shader> _shaders;
	private ShaderProgram _program;
	
	private Part _root;
	
	public ManeWindow(int width, int height)
		: base(GameWindowSettings.Default,
			new NativeWindowSettings() { ClientSize = (width, height), Title = "Window" })
	{
	}

	protected override void OnLoad()
	{
		GL.ClearColor(0.7f, 0.7f, 0.7f, 1.0f);
		
		_shaders = new ShaderLoader(ResourceDirectory)
			.LoadShaders();

		_program = new ShaderProgram();
		
		_program.LinkShaders(_shaders["shaders/.vert"], _shaders["shaders/.frag"]);

		var mesh = new QuadMesh();
		mesh.Create(100, 100);

		_root = new SpritePart()
		{
			Mesh = mesh,
			GlobalPosition = new Vector2(100, 100),
		};

		_viewport = new Viewport(_root, _program);
		
		_viewport.Resize(ClientSize);
	}

	protected override void OnRenderFrame(FrameEventArgs args)
	{
		GL.Clear(ClearBufferMask.ColorBufferBit);

		_viewport.Draw();
		
		SwapBuffers();
	}

	protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
	{
		GL.Viewport(0, 0, e.Width, e.Height);

		_viewport.Resize(e.Size);
	}
}