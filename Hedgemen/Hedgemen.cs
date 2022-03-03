using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Hgm.Modding;
using Hgm.Utilities;

namespace Hgm;

/// <summary>
/// The global repository for all Hedgemen state
/// </summary>
public static class Hedgemen
{
	private static bool _initialized = false;
	
	public static IHedgemen Proc { get; private set; }

	private static Dictionary<string, Assembly> _registeredAssemblies = new();
	
	public static Kaze Kaze { get; private set; }

	public static IReadOnlyDictionary<string, Assembly> RegisteredAssemblies => _registeredAssemblies;

	public static void RegisterAssembly(Type type)
	{
		lock (_registeredAssemblies)
		{
			var assemblyName = type.Assembly.GetName().Name;
			if (assemblyName == null)
				throw new InvalidOperationException($"Assembly name from {type} could not be discerned, " +
				                                    "this should not happen.");
			_registeredAssemblies.Add(assemblyName, type.Assembly);
		}
	}

	private static ILogger _logger;

	public static ILogger Logger => _logger;

	public static void RegisterAssemblies(params Type[] types)
	{
		foreach(var type in types)
			RegisterAssembly(type);
	}
	
	public static void RegisterAssemblies(IEnumerable<Type> types)
	{
		foreach(var type in types)
			RegisterAssembly(type);
	}

	[MethodImpl(MethodImplOptions.Synchronized)]
	public static void InitializeHedgemen(HedgemenInitStep init)
	{
		if (_initialized)
			throw new InvalidOperationException($"{typeof(Hedgemen)} is already initialized!");

		bool initStateProperlySet = init.IsProperlyInitialized();

		if (!initStateProperlySet)
			throw new ArgumentException($"'{nameof(init)}' is not properly initialized!");
		
		Proc = init.Proc;
		Kaze = new Kaze(init.KazeInitStep);
		_logger = init.Logger;
		_initialized = true;
	}

	/// <summary>
	/// Finishing step for Hedgemen. This will clear all game state and initialized data.
	/// </summary>
	[MethodImpl(MethodImplOptions.Synchronized)]
	public static void Finish()
	{
		Proc.Exit();
		Proc.Dispose();
		ClearState();
		_initialized = false;
	}

	private static void ClearState()
	{
		Proc = null;
		_registeredAssemblies.Clear();
	}
}

/// <summary>
/// Responsible for holding values to initialize Hedgemen. Values here should be initialized and handlers of
/// <see cref="HedgemenInitStep"/> should throw errors for invalid values.
/// </summary>
public struct HedgemenInitStep : IInitStep
{
	public IHedgemen Proc;
	public KazeInitStep KazeInitStep;
	public ILogger Logger;

	public bool IsProperlyInitialized() => 
		Proc != null &&
		KazeInitStep.IsProperlyInitialized() &&
		Logger != null;
}