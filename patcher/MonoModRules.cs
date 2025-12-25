using Mono.Cecil;
using MonoMod.InlineRT;
using System.Collections.Generic;
using System;

namespace MonoMod
{
    static partial class MonoModRules
    {
        static Dictionary<string, string> FMODMappings = new() {
            { "FMOD5_System_Create", "FMOD5_System_Create2" },
        };

        static MonoModRules()
        {
            Console.WriteLine($"[Oneshot.Wasm] Loaded into module {MonoModRule.Modder.Module}");
            MonoModRule.Modder.Log($"[Oneshot.Wasm] Loaded into module {MonoModRule.Modder.Module}");
            MonoModRule.Modder.PostProcessors += FMODPostProcessor;
        }

        public static void FMODPostProcessor(MonoModder modder)
        {
            foreach (TypeDefinition type in modder.Module.Types)
                foreach (MethodDefinition method in type.Methods)
                    FMODPostProcessMethod(modder, method);
        }
        public static void FMODPostProcessMethod(MonoModder modder, MethodDefinition method)
        {
            if (!method.HasBody && method.HasPInvokeInfo && method.PInvokeInfo.Module.Name.EndsWith("fmod"))
            {
				method.PInvokeInfo.Module.Name = "fmod";
                modder.LogVerbose($"[FMODPatcher] Wrapping {method.FullName} -> {method.PInvokeInfo.Module.Name}::{method.PInvokeInfo.EntryPoint}");
                if (FMODMappings.TryGetValue(method.PInvokeInfo.EntryPoint, out var remapped))
                {
                    method.PInvokeInfo.EntryPoint = remapped;
                }
            }
        }
    }
}
