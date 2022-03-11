using System;
using Hgm.Register;

namespace Hgm.Ecs.Text;

public interface ISchema
{
	public NamespacedString RegistryName { get; }
}

public static class SchemaExtensions
{
	public static T Instantiate<T>(this ISchema schema)
	{
		var value = default(T);
		var func = Hedgemen.Kaze.NewRegistry.Get<Func<T>>(schema.RegistryName);

		value = func();
		
		return value;
	}
}