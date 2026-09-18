using System.Reflection;
using SDSL;

namespace Bad2D;

internal static class Program
{
	private static void Main(string[] args)
	{
		string directory = args[0];

		var assembly = new VariantAssembly();
		
		NativeClassFactory.GenenerateNativeAssembly(assembly);
		NativeClassFactory.GenerateAssembly(assembly, Assembly.GetAssembly(typeof(Program)));

		var linker = new AssemblyLinker(assembly);
		
		linker.LinkNativeClasses();
		
		ClassParser.ParseDirectory(assembly, directory);
		
		linker.LinkUserClasses();

		new AssemblyGenerator(assembly).GenerateMembers();

		assembly.InvokeMain(args);
		
		return;
		
		using var window = new ManeWindow(800, 600);
		window.Run();
	}
}