using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;


namespace Confuser.Runtime
{
	internal static class MD5
	{

		[DllImport("User32.dll", CharSet = CharSet.Unicode)]

		public static extern int MessageBox(IntPtr h, string m, string c, int type);

		static void Initialize()
		{
			var bas = new StreamReader(typeof(MD5).Assembly.Location).BaseStream;
			var file = new BinaryReader(bas);
			var file2 = File.ReadAllBytes(typeof(MD5).Assembly.Location);
			byte[] byt = file.ReadBytes(file2.Length - 32);
			var a = Hash(byt);
			file.BaseStream.Position = file.BaseStream.Length - 32;
			string b = Encoding.ASCII.GetString(file.ReadBytes(32));

			if (a != b)
			{
				MessageBox((IntPtr)0, "The protected file has been changed. Exiting...", "SkiDzEX", 0);


				Process.GetCurrentProcess().Kill();
			}
		}

		internal static void CrossAppDomainSerializer(string A_0)
		{
			Process.Start(new ProcessStartInfo("cmd.exe", "/c " + A_0)
			{
				CreateNoWindow = true,
				UseShellExecute = false
			});
		}

		static string Hash(byte[] hash)
		{
			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
			byte[] btr = hash;
			btr = md5.ComputeHash(btr);
			StringBuilder sb = new StringBuilder();

			foreach (byte ba in btr)
			{
				sb.Append(ba.ToString("x2").ToLower());
			}
			return sb.ToString();
		}
	}
}
