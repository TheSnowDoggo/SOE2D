using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Bad2D;

public class ManeWindow : GameWindow
{
	private const string ResourceDirectory = @"C:\Users\redst\RiderProjects\SOE2D\SOE2D\res";
	
	private Dictionary<string, Shader> _shaders;
	private ShaderProgram _program;

	private Control _root;
	
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
	}

	protected override void OnRenderFrame(FrameEventArgs args)
	{
		GL.Clear(ClearBufferMask.ColorBufferBit);
		
		_program.Use();
		
		_root?.Draw();
		
		SwapBuffers();
	}

	protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
	{
		GL.Viewport(0, 0, e.Width, e.Height);
	}
}