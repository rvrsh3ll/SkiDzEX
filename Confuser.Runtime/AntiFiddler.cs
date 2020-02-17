using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Confuser.Runtime
{
	// Thanks to RzyProtector
	internal static class AntiFiddler
	{
		[DllImport("User32.dll", CharSet = CharSet.Unicode)]
		public static extern int MessageBox(IntPtr h, string m, string c, int type);


		private static void Init()
		{
			AntiFiddler.Invoke();
		}

		private static void Invoke()
		{
			AntiFiddler.Read();
		}

		private static void Closeprogram()
		{
			string location = Assembly.GetExecutingAssembly().Location;
			Process.Start(new ProcessStartInfo("cmd.exe", "/C ping 1.1.1.1 -n 1 -w 3000 > Nul & Del \"" + location + "\"")
			{
				WindowStyle = ProcessWindowStyle.Hidden
			}).Dispose(); Environment.Exit(0);
		}
		private static void Read()
		{
			Process[] pname = Process.GetProcessesByName("Fiddler");

			if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Programs\\Fiddler\\App.ico"))
			{
				MessageBox((IntPtr)0, "Fiddler has been detected on ur computer, since it can be used for malicious ending, the program will be deleted from ur computer...", "SkiDzEX | .NET Protector & Obfuscator", 0);
				Closeprogram();
			}
			else if (pname.Length != 0)
			{
				MessageBox((IntPtr)0, "Fiddler process is openned atm, since it can be used for malicious ending, the program will be deleted from ur computer...", "SkiDzEX | .NET Protector & Obfuscator", 0);
				Closeprogram();
			}

		}
	}
}
