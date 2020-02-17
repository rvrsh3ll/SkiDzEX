using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace Confuser.Runtime
{
	internal static class ForceElevation
	{
		[DllImport("User32.dll", CharSet = CharSet.Unicode)]

		public static extern int MessageBox(IntPtr h, string m, string c, int type);

		private static void Init()
		{
			if (!ForceElevation.Invoke())
			{
				MessageBox((IntPtr)0, " You need to start the program as admin.", "SkiDzEX | .NET Protector & Obfuscator", 0);

				Environment.Exit(0);
			}
		}

		public static bool Invoke()
		{
			return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
		}
	}
}
