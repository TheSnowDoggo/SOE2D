using SDSL;

namespace SOE2D;

public static class SOEClassGenerator
{
    public static void GenerateClasses(VariantAssembly assembly)
    {
        NativeClassFactory.GenerateClass<NativeVector2>(assembly, NativeVector2.Class);
        NativeClassFactory.GenerateClass<NativeColor>(assembly, NativeColor.Class);
        
        NativeClassFactory.GenerateClass<PackedPartView>(assembly, PackedPartView.Class);
        NativeClassFactory.GenerateClass<Part>(assembly, Part.Class);
    }
}