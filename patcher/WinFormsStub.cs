using System;

namespace MonoMod
{
    [MonoModLinkFrom("System.Windows.Forms.MessageBox")]
    public class MessageBox
    {
        public static MessageBoxResult Show(string text)
        {
            Console.Error.WriteLine(text);
			return MessageBoxResult.None;
        }
    }

    [MonoModLinkFrom("System.Windows.Forms.Screen")]
    public class Screen
    {
        public static Screen[] AllScreens { get { return []; } }
		public Rectangle Bounds { get { return new(); } }
    }

	[MonoModLinkFrom("System.Drawing.Rectangle")]
	public class Rectangle {
		public int Width { get; set; }
		public int Height { get; set; }
	}

    [MonoModLinkFrom("System.Windows.Forms.DialogResult")]
    public enum MessageBoxResult
    {
		None = 0,
		OK = 1,
		Cancel = 2,
		Abort = 3,
		Retry = 4,
		Ignore = 5,
		Yes = 6,
		No = 7,
		TryAgain = 10,
		Continue = 11,
    }
}
