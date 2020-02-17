using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace Confuser.Runtime
{
    internal static class ProcessMonitor
    {
        static unsafe void Init()
        {
            Thread workerThread = new Thread(() => DoWork());
            workerThread.IsBackground = true;
            workerThread.Start();
        }
        static unsafe void DoWork()
        {
            string[] array = new string[]
            {
                "solarwinds",
                "paessler",
                "cpacket",
                "wireshark",
                "Ethereal",
                "sectools",
                "riverbed",
                "tcpdump",
                "Kismet",
                "EtherApe",
                "Fiddler",
                "telerik",
                "glasswire",
                "HTTPDebuggerSvc",
                "HTTPDebuggerUI",
                "charles",
                "intercepter",
                "snpa",
                "dumcap",
                "comview",
                "netcheat",
                "cheat",
                "winpcap",
                "megadumper",
                "MegaDumper",
                "dnspy",
                "ilspy",
                "reflector"
            };
            if (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)).Length > 2)
            {
                Process.GetCurrentProcess().Kill();
            }
            while (true)
            {
                foreach (string processName in array)
                {
                    Process[] processesByName = Process.GetProcessesByName(processName);
                    if (processesByName.Length != 0)
                    {
                        Environment.Exit(0);
                    }
                }
                foreach (Process proc in Process.GetProcesses())
                {
                    foreach (string processName in array)
                    {
                        bool x = false;
                        if (proc.MainWindowTitle.ToLower().Contains(processName) || proc.ProcessName.ToLower().Contains(processName) || proc.MainWindowTitle.ToLower().Contains(processName) || proc.ProcessName.ToLower().Contains(processName))
                            x = true;
                        if (x) {
                            try
                            {
                                proc.Kill();
                            }
                            catch
                            {
                                Environment.Exit(0);
                            }
                        }
                    }
                }
                Thread.Sleep(3000);
            }
        }
    }
}