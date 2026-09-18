using SDSL;

namespace Bad2D;

internal static class Program
{
	private static void Main(string[] args)
	{
		using var window = new ManeWindow(800, 600);
		window.Run();
	}

	private static void RunScript(string directory)
	{
		var assembly = new VariantAssembly();
		
		NativeClassFactory.GenenerateNativeClasses(assembly);
		
		SOEClassGenerator.GenerateClasses(assembly);

		var linker = new AssemblyLinker(assembly);
		
		linker.LinkNativeClasses();
		
		ClassParser.ParseDirectory(assembly, directory);
		
		linker.LinkUserClasses();

		new AssemblyGenerator(assembly).GenerateMembers();

		assembly.InvokeMain();
	}
}