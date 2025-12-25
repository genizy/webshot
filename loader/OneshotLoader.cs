using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using System.Reflection;

using Microsoft.Xna.Framework;

struct Dll
{
    public string RealName;
    public string MappedName;
}

public static partial class OneshotLoader
{
	private const string CONFIG_INI = """
		[paths]
		content=/libsdl/OneShot/content
		gamedata=/libsdl/OneShot/gamedata
		""";

    private static void Main()
    {
        Console.WriteLine(":3");
    }

    private static void TryCreateDirectory(string path)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }

    private static void ForceCopy(string from, string to)
    {
        if (File.Exists(to))
			File.Delete(to);
		File.Copy(from, to);
    }

    private static void MountDlls(string root, string[] rawDlls)
    {
        IEnumerable<Dll> dlls = rawDlls.Select(x =>
        {
            var split = x.Split('|');
            return new Dll() { RealName = split[0], MappedName = split[1] };
        });

        // mono.cecil searches in /bin for some dlls
        Directory.CreateDirectory("/bin");
        Emscripten.MountFetch(0, root + "_framework/", "/fetchdlls/");
        foreach (var dll in dlls)
        {
            Emscripten.MountFetchFile(0, $"/fetchdlls/{dll.RealName}");
            File.CreateSymbolicLink($"/bin/{dll.MappedName}", $"/fetchdlls/{dll.RealName}");
        }
    }

    [JSExport]
    internal static Task PreInit(string root, string[] rawDlls)
    {
        try
        {
            Environment.SetEnvironmentVariable("MONOMOD_DEPENDENCY_REMOVE_PATCH", "0");

			Emscripten.MountOpfs();
            MountDlls(root, rawDlls);
			TryCreateDirectory("/libsdl/Saves/");
            File.CreateSymbolicLink("/bin/OneShotMG.exe", "/libsdl/OneShot.dll");
			File.CreateSymbolicLink("/OneShotWME", "/libsdl/Saves");
			File.WriteAllText("/config.ini", CONFIG_INI);

            MonoMod.Core.Platforms.WasmDetourFactory.EnableTailCallDetours = true;

            return Task.Delay(0);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine("Error in PreInit()!");
            Console.Error.WriteLine(e);
            return Task.FromException(e);
        }
    }

    static Game Game;
	static object GamePlatform;
    static FieldInfo GamePlatform_IsExiting;
    static MethodInfo GamePlatform_SdlRunLoop;
    static MethodInfo Threading_Run;
    static MethodInfo GraphicsDevice_DisposeContexts;
    static MethodInfo Game_RunOneFrame;

    [JSExport]
    internal static Task Init()
    {
        try
        {
			ForceCopy("/libsdl/OneShot/content/npc/DOORS2.xnb", "/libsdl/OneShot/content/npc/doors2.xnb");
			ForceCopy("/libsdl/OneShot/content/npc/DOORS.xnb", "/libsdl/OneShot/content/npc/doors.xnb");

			var OneShotMG = Assembly.LoadFrom("/libsdl/OneShot.dll");
			var MonoGame_Framework = typeof(Game).Assembly;

			var Game1 = OneShotMG.GetType("OneShotMG.Game1");
			Game = (Game)Game1.GetConstructor([]).Invoke([]);

            Game_RunOneFrame = Game1.GetMethod("RunOneFrame", BindingFlags.Public | BindingFlags.Instance);

            var Game_Platform = Game1.GetField("Platform", BindingFlags.NonPublic | BindingFlags.Instance);
            var SdlGamePlatform = MonoGame_Framework.GetType("Microsoft.Xna.Framework.SdlGamePlatform");
            GamePlatform = Game_Platform.GetValue(Game);
            GamePlatform_IsExiting = SdlGamePlatform.GetField("_isExiting", BindingFlags.NonPublic | BindingFlags.Instance);
            GamePlatform_SdlRunLoop = SdlGamePlatform.GetMethod("SdlRunLoop", BindingFlags.NonPublic | BindingFlags.Instance);

            var Threading = MonoGame_Framework.GetType("Microsoft.Xna.Framework.Threading");
            Threading_Run = Threading.GetMethod("Run", BindingFlags.NonPublic | BindingFlags.Static);

            var GraphicsDevice = MonoGame_Framework.GetType("Microsoft.Xna.Framework.Graphics.GraphicsDevice");
            GraphicsDevice_DisposeContexts = GraphicsDevice.GetMethod("DisposeContexts", BindingFlags.NonPublic | BindingFlags.Static);

            return Task.Delay(0);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine("Error in Init()!");
            Console.Error.WriteLine(e);
            return Task.FromException(e);
        }
    }

    internal static bool _RunOneFrame()
    {
        GamePlatform_SdlRunLoop.Invoke(GamePlatform, []);
        Game_RunOneFrame.Invoke(Game, []);
        Threading_Run.Invoke(null, []);
        GraphicsDevice_DisposeContexts.Invoke(null, []);

        return (int)GamePlatform_IsExiting.GetValue(GamePlatform) == 0;
    }

    [JSExport]
    internal static Task<bool> RunOneFrame()
    {
        try
        {
            return Task.FromResult(_RunOneFrame());
        }
        catch (Exception e)
        {
            Console.Error.WriteLine("Error in RunOneFrame()!");
            Console.Error.WriteLine(e);
            return (Task<bool>)Task.FromException(e);
        }
    }

    [JSExport]
    internal static Task MainLoop()
    {
        try
        {
            return Emscripten.RunEmLoop(() =>
            {
                try
                {
                    var keepGoing = _RunOneFrame();
                    return keepGoing;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine("Error in MainLoop()!");
                    Console.Error.WriteLine(e);
                    throw;
                }
            });
        }
        catch (Exception e)
        {
            Console.Error.WriteLine("Error in MainLoop() RunEmLoop!");
            Console.Error.WriteLine(e);
            return Task.FromException(e);
        }
    }

    [JSExport]
    internal static Task Cleanup()
    {
        try
        {
            ((IDisposable)Game).Dispose();
            return Task.Delay(0);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine("Error in Cleanup()!");
            Console.Error.WriteLine(e);
            return Task.FromException(e);
        }
    }
}
