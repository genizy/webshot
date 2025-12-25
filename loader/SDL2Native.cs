using System;
using System.Runtime.InteropServices;

internal static partial class SDL2Native
{
    // Enums and Structs copied from SDL2.cs to support DllImport signatures

    [Flags]
    public enum InitFlags
    {
        Video = 0x00000020,
        Joystick = 0x00000200,
        Haptic = 0x00001000,
        GameController = 0x00002000,
    }

    public enum EventType : uint
    {
        First = 0, Quit = 0x100, WindowEvent = 0x200, SysWM = 0x201, KeyDown = 0x300, KeyUp = 0x301, TextEditing = 0x302, TextInput = 0x303, MouseMotion = 0x400, MouseButtonDown = 0x401, MouseButtonup = 0x402, MouseWheel = 0x403, JoyAxisMotion = 0x600, JoyBallMotion = 0x601, JoyHatMotion = 0x602, JoyButtonDown = 0x603, JoyButtonUp = 0x604, JoyDeviceAdded = 0x605, JoyDeviceRemoved = 0x606, ControllerAxisMotion = 0x650, ControllerButtonDown = 0x651, ControllerButtonUp = 0x652, ControllerDeviceAdded = 0x653, ControllerDeviceRemoved = 0x654, ControllerDeviceRemapped = 0x654, FingerDown = 0x700, FingerUp = 0x701, FingerMotion = 0x702, DollarGesture = 0x800, DollarRecord = 0x801, MultiGesture = 0x802, ClipboardUpdate = 0x900, DropFile = 0x1000, DropText = 0x1001, DropBegin = 0x1002, DropComplete = 0x1003, AudioDeviceAdded = 0x1100, AudioDeviceRemoved = 0x1101, RenderTargetsReset = 0x2000, RenderDeviceReset = 0x2001, UserEvent = 0x8000, Last = 0xFFFF
    }

    [StructLayout(LayoutKind.Explicit, Size = 56)]
    public struct Event
    {
        [FieldOffset(0)]
        public EventType Type;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Rectangle
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Version
    {
        public byte Major;
        public byte Minor;
        public byte Patch;
    }

    public enum SysWMType
    {
        Unknow, Windows, X11, Directfb, Cocoa, UiKit, Wayland, Mir, WinRt, Android
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_SysWMinfo
    {
        public Version version;
        public SysWMType subsystem;
        public IntPtr window;
    }

    public enum GLAttribute
    {
        RedSize, GreenSize, BlueSize, AlphaSize, BufferSize, DoubleBuffer, DepthSize, StencilSize, AccumRedSize, AccumGreenSize, AccumBlueSize, AccumAlphaSize, Stereo, MultiSampleBuffers, MultiSampleSamples, AcceleratedVisual, RetainedBacking, ContextMajorVersion, ContextMinorVersion, ContextEgl, ContextFlags, ContextProfileMAsl, ShareWithCurrentContext, FramebufferSRGBCapable, ContextReleaseBehaviour
    }

    [Flags]
    public enum MouseButton
    {
        Left = 1 << 0, Middle = 1 << 1, Right = 1 << 2, X1Mask = 1 << 3, X2Mask = 1 << 4
    }

    public enum SystemCursor
    {
        Arrow, IBeam, Wait, Crosshair, WaitArrow, SizeNWSE, SizeNESW, SizeWE, SizeNS, SizeAll, No, Hand
    }

    [Flags]
    public enum Keymod : ushort
    {
        None = 0x0000, LeftShift = 0x0001, RightShift = 0x0002, LeftCtrl = 0x0040, RightCtrl = 0x0080, LeftAlt = 0x0100, RightAlt = 0x0200, LeftGui = 0x0400, RightGui = 0x0800, NumLock = 0x1000, CapsLock = 0x2000, AltGr = 0x4000, Reserved = 0x8000, Ctrl = (LeftCtrl | RightCtrl), Shift = (LeftShift | RightShift), Alt = (LeftAlt | RightAlt), Gui = (LeftGui | RightGui)
    }

    [Flags]
    public enum Hat : byte
    {
        Centered = 0, Up = 1 << 0, Right = 1 << 1, Down = 1 << 2, Left = 1 << 3
    }

    public enum GameControllerAxis
    {
        Invalid = -1, LeftX, LeftY, RightX, RightY, TriggerLeft, TriggerRight, Max
    }

    public enum GameControllerButton
    {
        Invalid = -1, A, B, X, Y, Back, Guide, Start, LeftStick, RightStick, LeftShoulder, RightShoulder, DpadUp, DpadDown, DpadLeft, DpadRight, Max
    }

    public enum HapticEffectId : ushort
    {
        LeftRight = (1 << 2)
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HapticLeftRight
    {
        public HapticEffectId Type;
        public uint Length;
        public ushort LargeMagnitude;
        public ushort SmallMagnitude;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct HapticEffect
    {
        [FieldOffset(0)] public HapticEffectId type;
        [FieldOffset(0)] public HapticLeftRight leftright;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DisplayMode
    {
        public uint Format;
        public int Width;
        public int Height;
        public int RefreshRate;
        public IntPtr DriverData;
    }

    private const string nativeLibName = "libSDL2";

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_Init")]
    public static extern int SDL_Init(int flags);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_DisableScreenSaver")]
    public static extern void SDL_DisableScreenSaver();
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetVersion")]
    public static extern void SDL_GetVersion(out Version version);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_PollEvent")]
    public static extern int SDL_PollEvent([Out] out Event _event);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_PumpEvents")]
    public static extern int SDL_PumpEvents();
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_CreateRGBSurfaceFrom")]
    public static extern IntPtr SDL_CreateRGBSurfaceFrom(IntPtr pixels, int width, int height, int depth, int pitch, uint rMask, uint gMask, uint bMask, uint aMask);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_FreeSurface")]
    public static extern void SDL_FreeSurface(IntPtr surface);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetError")]
    public static extern IntPtr SDL_GetError();
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_ClearError")]
    public static extern void SDL_ClearError();
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetHint")]
    public static extern IntPtr SDL_GetHint([MarshalAs(UnmanagedType.LPUTF8Str)] string name);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_LoadBMP_RW")]
    public static extern IntPtr SDL_LoadBMP_RW(IntPtr src, int freesrc);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_Quit")]
    public static extern void SDL_Quit();
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_RWFromMem")]
    public static extern IntPtr SDL_RWFromMem(byte[] mem, int size);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetHint")]
    public static extern int SDL_SetHint([MarshalAs(UnmanagedType.LPUTF8Str)] string name, [MarshalAs(UnmanagedType.LPUTF8Str)] string value);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_CreateWindow")]
    public static extern IntPtr SDL_CreateWindow([MarshalAs(UnmanagedType.LPUTF8Str)] string title, int x, int y, int w, int h, int flags);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_DestroyWindow")]
    public static extern void SDL_DestroyWindow(IntPtr window);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetWindowID")]
    public static extern uint SDL_GetWindowID(IntPtr window);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetWindowDisplayIndex")]
    public static extern int SDL_GetWindowDisplayIndex(IntPtr window);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetWindowFlags")]
    public static extern int SDL_GetWindowFlags(IntPtr window);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetWindowIcon")]
    public static extern void SDL_SetWindowIcon(IntPtr window, IntPtr icon);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetWindowPosition")]
    public static extern void SDL_GetWindowPosition(IntPtr window, out int x, out int y);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetWindowSize")]
    public static extern void SDL_GetWindowSize(IntPtr window, out int w, out int h);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetWindowBordered")]
    public static extern void SDL_SetWindowBordered(IntPtr window, int bordered);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetWindowFullscreen")]
    public static extern int SDL_SetWindowFullscreen(IntPtr window, int flags);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetWindowPosition")]
    public static extern void SDL_SetWindowPosition(IntPtr window, int x, int y);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetWindowResizable")]
    public static extern void SDL_SetWindowResizable(IntPtr window, [MarshalAs(UnmanagedType.Bool)] bool resizable);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetWindowSize")]
    public static extern void SDL_SetWindowSize(IntPtr window, int w, int h);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetWindowTitle")]
    public static extern void SDL_SetWindowTitle(IntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string value);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_ShowWindow")]
    public static extern void SDL_ShowWindow(IntPtr window);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetWindowWMInfo")]
    public static extern bool SDL_GetWindowWMInfo(IntPtr window, ref SDL_SysWMinfo sysWMinfo);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetWindowBordersSize")]
    public static extern int SDL_GetWindowBordersSize(IntPtr window, out int top, out int left, out int right, out int bottom);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetDisplayBounds")]
    public static extern int SDL_GetDisplayBounds(int displayIndex, out Rectangle rect);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetCurrentDisplayMode")]
    public static extern int SDL_GetCurrentDisplayMode(int displayIndex, out DisplayMode mode);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetDisplayMode")]
    public static extern int SDL_GetDisplayMode(int displayIndex, int modeIndex, out DisplayMode mode);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetClosestDisplayMode")]
    public static extern int SDL_GetClosestDisplayMode(int displayIndex, DisplayMode mode, out DisplayMode closest);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetDisplayName")]
    public static extern IntPtr SDL_GetDisplayName(int index);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetNumDisplayModes")]
    public static extern int SDL_GetNumDisplayModes(int displayIndex);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetNumVideoDisplays")]
    public static extern int SDL_GetNumVideoDisplays();
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GL_CreateContext")]
    public static extern IntPtr SDL_GL_CreateContext(IntPtr window);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GL_DeleteContext")]
    public static extern void SDL_GL_DeleteContext(IntPtr context);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GL_GetCurrentContext")]
    public static extern IntPtr SDL_GL_GetCurrentContext();
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GL_GetProcAddress")]
    public static extern IntPtr SDL_GL_GetProcAddress([MarshalAs(UnmanagedType.LPUTF8Str)] string proc);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GL_GetSwapInterval")]
    public static extern int SDL_GL_GetSwapInterval();
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GL_MakeCurrent")]
    public static extern int SDL_GL_MakeCurrent(IntPtr window, IntPtr context);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GL_SetAttribute")]
    public static extern int SDL_GL_SetAttribute(GLAttribute attr, int value);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GL_SetSwapInterval")]
    public static extern int SDL_GL_SetSwapInterval(int interval);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GL_SwapWindow")]
    public static extern void SDL_GL_SwapWindow(IntPtr window);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_CreateColorCursor")]
    public static extern IntPtr SDL_CreateColorCursor(IntPtr surface, int x, int y);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_CreateSystemCursor")]
    public static extern IntPtr SDL_CreateSystemCursor(SystemCursor id);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_FreeCursor")]
    public static extern void SDL_FreeCursor(IntPtr cursor);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetGlobalMouseState")]
    public static extern MouseButton SDL_GetGlobalMouseState(out int x, out int y);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetMouseState")]
    public static extern MouseButton SDL_GetMouseState(out int x, out int y);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetCursor")]
    public static extern void SDL_SetCursor(IntPtr cursor);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_ShowCursor")]
    public static extern int SDL_ShowCursor(int toggle);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_WarpMouseInWindow")]
    public static extern void SDL_WarpMouseInWindow(IntPtr window, int x, int y);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetModState")]
    public static extern Keymod SDL_GetModState();
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_JoystickClose")]
    public static extern void SDL_JoystickClose(IntPtr joystick);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_JoystickFromInstanceID")]
    public static extern IntPtr SDL_JoystickFromInstanceID(int joyid);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_JoystickGetAxis")]
    public static extern short SDL_JoystickGetAxis(IntPtr joystick, int axis);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_JoystickGetButton")]
    public static extern byte SDL_JoystickGetButton(IntPtr joystick, int button);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_JoystickName")]
    public static extern IntPtr SDL_JoystickName(IntPtr joystick);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_JoystickGetGUID")]
    public static extern Guid SDL_JoystickGetGUID(IntPtr joystick);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_JoystickGetHat")]
    public static extern Hat SDL_JoystickGetHat(IntPtr joystick, int hat);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_JoystickInstanceID")]
    public static extern int SDL_JoystickInstanceID(IntPtr joystick);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_JoystickOpen")]
    public static extern IntPtr SDL_JoystickOpen(int deviceIndex);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_JoystickNumAxes")]
    public static extern int SDL_JoystickNumAxes(IntPtr joystick);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_JoystickNumButtons")]
    public static extern int SDL_JoystickNumButtons(IntPtr joystick);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_JoystickNumHats")]
    public static extern int SDL_JoystickNumHats(IntPtr joystick);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_NumJoysticks")]
    public static extern int SDL_NumJoysticks();
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_free")]
    public static extern void SDL_free(IntPtr ptr);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerAddMapping")]
    public static extern int SDL_GameControllerAddMapping([MarshalAs(UnmanagedType.LPUTF8Str)] string mappingString);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerAddMappingsFromRW")]
    public static extern int SDL_GameControllerAddMappingsFromRW(IntPtr rw, int freew);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerClose")]
    public static extern void SDL_GameControllerClose(IntPtr gamecontroller);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerFromInstanceID")]
    public static extern IntPtr SDL_GameControllerFromInstanceID(int joyid);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerGetAxis")]
    public static extern short SDL_GameControllerGetAxis(IntPtr gamecontroller, GameControllerAxis axis);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerGetButton")]
    public static extern byte SDL_GameControllerGetButton(IntPtr gamecontroller, GameControllerButton button);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerGetJoystick")]
    public static extern IntPtr SDL_GameControllerGetJoystick(IntPtr gamecontroller);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_IsGameController")]
    public static extern byte SDL_IsGameController(int joystickIndex);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerMapping")]
    public static extern IntPtr SDL_GameControllerMapping(IntPtr gamecontroller);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerOpen")]
    public static extern IntPtr SDL_GameControllerOpen(int joystickIndex);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerName")]
    public static extern IntPtr SDL_GameControllerName(IntPtr gamecontroller);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerRumble")]
    public static extern int SDL_GameControllerRumble(IntPtr gamecontroller, ushort left, ushort right, uint duration);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerRumbleTriggers")]
    public static extern int SDL_GameControllerRumbleTriggers(IntPtr gamecontroller, ushort left, ushort right, uint duration);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerHasRumble")]
    public static extern byte SDL_GameControllerHasRumble(IntPtr gamecontroller);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerHasRumbleTriggers")]
    public static extern byte SDL_GameControllerHasRumbleTriggers(IntPtr gamecontroller);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_HapticClose")]
    public static extern void SDL_HapticClose(IntPtr haptic);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_HapticEffectSupported")]
    public static extern int SDL_HapticEffectSupported(IntPtr haptic, ref HapticEffect effect);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_JoystickIsHaptic")]
    public static extern int SDL_JoystickIsHaptic(IntPtr joystick);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_HapticNewEffect")]
    public static extern int SDL_HapticNewEffect(IntPtr haptic, ref HapticEffect effect);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_HapticOpen")]
    public static extern IntPtr SDL_HapticOpen(int device_index);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_HapticOpenFromJoystick")]
    public static extern IntPtr SDL_HapticOpenFromJoystick(IntPtr joystick);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_HapticRumbleInit")]
    public static extern int SDL_HapticRumbleInit(IntPtr haptic);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_HapticRumblePlay")]
    public static extern int SDL_HapticRumblePlay(IntPtr haptic, float strength, uint length);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_HapticRumbleSupported")]
    public static extern int SDL_HapticRumbleSupported(IntPtr haptic);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_HapticRunEffect")]
    public static extern int SDL_HapticRunEffect(IntPtr haptic, int effect, uint iterations);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_HapticStopAll")]
    public static extern int SDL_HapticStopAll(IntPtr haptic);
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_HapticUpdateEffect")]
    public static extern int SDL_HapticUpdateEffect(IntPtr haptic, int effect, ref HapticEffect data);
}
