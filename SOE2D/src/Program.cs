namespace SOE2D;

internal static class Program
{
	private static void Main(string[] args)
	{
		using var window = new ManeWindow(800, 600, args[0]);
		window.Run();
	}
}