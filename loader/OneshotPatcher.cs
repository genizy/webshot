using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using System.Linq;

using MonoMod;
using Mono.Cecil;
using Mono.Cecil.Rocks;

public partial class OneshotPatcher
{
	[JSExport]
	public static async Task Patch()
	{
		try
		{
            if (File.Exists("/libsdl/OneShot/OneShotMGMac.dll")) {
                Console.WriteLine("Mac version detected");
                Patch("/libsdl/OneShot/OneShotMGMac.dll", "/libsdl/OneShot.dll");
            } else {
			    Patch("/libsdl/OneShot/OneShotMG.exe", "/libsdl/OneShot.dll");
            }
		} catch (Exception e)
		{
            Console.Error.WriteLine("Error in Patch()!");
            Console.Error.WriteLine(e);
			throw;
		}
	}

    private static void RunMonoModAuto(ModuleDefinition module, List<ModuleReference> mods, Action<MonoModder> callback)
    {
        RunMonoMod(module, mods, modder =>
        {
            modder.MapDependencies();
            callback(modder);
            modder.AutoPatch();
        });
    }

    private static void RunMonoMod(ModuleDefinition module, List<ModuleReference> mods, Action<MonoModder> callback)
    {
        using (MonoModder modder = new()
        {
            Module = module,
            Mods = mods,
            MissingDependencyThrow = false,
            LogVerboseEnabled = false,
        })
        {
            modder.DependencyDirs.Add("/bin");
            modder.DependencyDirs.Add("/libsdl/OneShot/");

            callback(modder);
        }
    }

    private static void Patch(string path, string output)
    {
        ReaderParameters parameters = new(ReadingMode.Immediate) { ReadSymbols = false, InMemory = true };
        var target = ModuleDefinition.ReadModule(new MemoryStream(File.ReadAllBytes(path)), parameters);

        var mod = ModuleDefinition.ReadModule("/bin/Oneshot.Wasm.mm.dll");

        RunMonoModAuto(target, [mod], modder =>
        {
            modder.Log($"Patching {path}");
        });
        
        try {
            var winFormsRef = target.AssemblyReferences.First(r => r.Name == "System.Windows.Forms");
            target.AssemblyReferences.Remove(winFormsRef);
        } catch(InvalidOperationException) {
            Console.WriteLine("winforms references not found, skipping since this is probably not a windows build");
        }

        target.Write(output, new WriterParameters() { WriteSymbols = false });
    }
}
