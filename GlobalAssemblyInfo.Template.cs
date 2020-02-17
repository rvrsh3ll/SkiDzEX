using System.Reflection;

[assembly: AssemblyProduct("Discord Link : ")]
[assembly: AssemblyCompany("Ki")]
[assembly: AssemblyCopyright("Copyright (C) Ki 2014")]

#if DEBUG

[assembly: AssemblyConfiguration("Debug")]
#else

[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyVersion("{{VER}}")]
[assembly: AssemblyFileVersion("{{VER}}")]
[assembly: AssemblyInformationalVersion("{{TAG}}")]