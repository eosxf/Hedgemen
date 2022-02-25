using System;
using System.Collections.Generic;
using System.Reflection;

namespace Hgm;

public static class Hedgemen
{
	private static IHedgemen _proc;

	public static IHedgemen Proc
	{
		get => _proc;
		internal set => _proc = value;
	}
	
	private static Dictionary<string, Assembly> _registeredAssemblies = new();

	public static Dictionary<string, Assembly> RegisteredAssemblies
	{
		get => _registeredAssemblies;
		internal set => _registeredAssemblies = value;
	}

	public static void RegisterAssembly(Type type)
	{
		_registeredAssemblies.Add(type.Assembly.FullName, type.Assembly);
	}

	public static bool IsProperlyInitialized() => _proc != null;

	public static void Finish()
	{
		_proc.Exit();
		_proc.Dispose();
		_proc = null;
	}
}