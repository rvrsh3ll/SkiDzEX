using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Confuser.Runtime
{
	// Thanks to Rzy
	internal static class AntiHttpDebugger
	{
		[DllImport("User32.dll", CharSet = CharSet.Unicode)]
		public static extern int MessageBox(IntPtr h, string m, string c, int type);


		private static void Init()
		{
			AntiHttpDebugger.Invoke();
		}

		private static void Invoke()
		{
			AntiHttpDebugger.Read();
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

			if (File.Exists("C:\\Program Files (x86)\\HTTPDebuggerPro\\HTTPDebuggerBrowser.dll"))
			{
				MessageBox((IntPtr)0, "Http Debugger has been detected on ur computer, since it can be used for malicious ending, the program will be deleted from ur computer...", "SkiDzEX | .NET Protector & Obfuscator", 0);
				Closeprogram();
			}
			else if (File.Exists("D:\\Program Files (x86)\\HTTPDebuggerPro\\HTTPDebuggerBrowser.dll"))
			{
				MessageBox((IntPtr)0, "Http Debugger has been detected on ur computer, since it can be used for malicious ending, the program will be deleted from ur computer...", "SkiDzEX | .NET Protector & Obfuscator", 0);
				Closeprogram();
			}
			else if (File.Exists("F:\\Program Files (x86)\\HTTPDebuggerPro\\HTTPDebuggerBrowser.dll"))
			{
				MessageBox((IntPtr)0, "Http Debugger has been detected on ur computer, since it can be used for malicious ending, the program will be deleted from ur computer...", "SkiDzEX | .NET Protector & Obfuscator", 0);
				Closeprogram();
			}


		}
	}
}
