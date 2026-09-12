using System.Reflection;
using SDSL;
using SDSL.Factory;
using SDSL.Functions;
using SDSL.Prototypes;
using SDSL.Statements;

namespace Bad2D;

internal static class Program
{
	private const string ScriptDirectory = @"C:\Users\redst\RiderProjects\Bad2D\Bad2D\res\scripts";
	
	private static void Main(string[] args)
	{
		var pAssembly = new PrototypeAssembly("Bad");

		pAssembly.GlobalUsings.Add(GlobalConfig.Global);
		
		SealClassFactory.GenerateNativeClasses(pAssembly);
		
		SealClassFactory.GenerateExportedClasses(pAssembly,
			Assembly.GetAssembly(typeof(Program)));
		
		PrototypeParser.ParseProjectDirectory(pAssembly, ScriptDirectory);

		SealAssembly assembly = new AssemblyGenerator(pAssembly)
			.GenerateAssembly();

		assembly.InvokeMain();
		
		return;
		
		using var window = new ManeWindow(800, 600);
		window.Run();
	}
}