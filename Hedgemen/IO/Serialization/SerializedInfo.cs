using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Hgm.IO.Serialization;

[Serializable]
public class SerializedInfo : IHasSerializedFields
{
	private string _assemblyName;

	private SerializedFields _fields;
	private string _typeName;

	public SerializedInfo(ISerializableInfo obj, SerializedFields fields)
	{
		if (obj is null) throw new ArgumentNullException(nameof(obj));
		_assemblyName = obj.GetType().Assembly.GetName().Name;
		_typeName = obj.GetType().FullName;
		_fields = fields;
	}
	
	public SerializedInfo(Type type, SerializedFields fields)
	{
		if (type is null) throw new ArgumentNullException(nameof(type));
		_assemblyName = type.Assembly.GetName().Name;
		_typeName = type.FullName;
		_fields = fields;
	}

	public SerializedInfo()
	{
	}

	[JsonInclude]
	[JsonPropertyName("assembly_name")]
	public string AssemblyName
	{
		get => _assemblyName;
		private set => _assemblyName = value;
	}

	[JsonInclude]
	[JsonPropertyName("type_name")]
	public string TypeName
	{
		get => _typeName;
		private set => _typeName = value;
	}

	[JsonInclude]
	[JsonPropertyName("fields")]
	public SerializedFields Fields
	{
		get => _fields;
		private set => _fields = value;
	}

	public T Instantiate<T>(IReadOnlyDictionary<string, Assembly> registeredAssemblies = null, bool init = true)
		where T : ISerializableInfo
	{
		return (T) Instantiate(registeredAssemblies, init);
	}

	public ISerializableInfo Instantiate(IReadOnlyDictionary<string, Assembly> registeredAssemblies = null,
										 bool init = true)
	{
		registeredAssemblies ??= Hedgemen.RegisteredAssemblies;
		// we need a way to access assemblies because NativeAoT doesn't allow you to get assemblies via
		// Activator.CreateInstance(assemblyName, typeName). Aka, eat my ass, NativeAoT. Jk, love you
		var assembly = registeredAssemblies[_assemblyName];
		var obj = (assembly.CreateInstance(_typeName) as ISerializableInfo)!;
		if(init)
			obj.ReadSerializedInfo(this);
		return obj;
	}
}