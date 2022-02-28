using System;
using System.Collections.Generic;
using System.Reflection;

namespace Hgm;

public static class Hedgemen
{
	public static IHedgemen Proc { get; internal set; }

	public static Dictionary<string, Assembly> RegisteredAssemblies { get; internal set; } = new();

	public static void RegisterAssembly(Type type)
	{
		RegisteredAssemblies.Add(type.Assembly.FullName, type.Assembly);
	}

	public static bool IsProperlyInitialized()
	{
		return Proc != null;
	}

	public static void Finish()
	{
		Proc.Exit();
		Proc.Dispose();
		Proc = null;
	}
}