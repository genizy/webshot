using MonoMod;
using System;


// this has gotta be one of the worst ways to possibly do this... - fish
namespace OneShotMG.src.TWM
{
    [MonoModPatch("global::OneShotMG.src.TWM.DocumentWindow")]
    public class DocumentWindow
    {
        [MonoModIgnore]
        private static extern string orig_GetFileTxt(string docFile);

        private static string GetFileTxt(string docFile)
        {
            // Console.WriteLine("congrats, the detour worked!");
            if (docFile.Contains("credits_pc"))
            {
                Console.WriteLine("injecting wasm credits >:3");
                string creditsWeb = @"
WASM Port
----------

* r58playz
* bomberfish
";
                string origCredits = orig_GetFileTxt(docFile);
                return origCredits.Replace("=======", "=======\n" + creditsWeb);
            } else 
            {
                return orig_GetFileTxt(docFile);
            }
        }
    }
}