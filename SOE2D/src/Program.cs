using System.Diagnostics;
using System.Reflection;
using SDSL;
using SixLabors.ImageSharp;

namespace SOE2D;

internal static class Program
{
	private static void Main(string[] args)
	{
		//RunScript(@"C:\Users\redst\RiderProjects\Bad2D\SOE2D\res\scripts"); return;
		
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