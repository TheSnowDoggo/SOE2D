using SDSL;

namespace Bad2D;

public static class SOEClassGenerator
{
    public static void GenerateClasses(VariantAssembly assembly)
    {
        NativeClassFactory.GenerateClass<NativeVector2>(assembly, NativeVector2.Class);
    }
}