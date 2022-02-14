using System.Collections.Generic;
using System.Reflection;

namespace Hgm
{
	public sealed class Globals
	{
		private Dictionary<string, Assembly> _registeredAssemblies = new();

		public IReadOnlyDictionary<string, Assembly> RegisteredAssemblies => _registeredAssemblies;

		public void RegisterAssembly(Assembly assembly)
		{
			_registeredAssemblies.Add(assembly.FullName, assembly);
		}
	}
}