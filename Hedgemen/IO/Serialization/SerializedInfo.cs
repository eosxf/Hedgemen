using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Hgm.IO.Serialization;

[Serializable]
public class SerializedInfo
{
	private string _assemblyName;
	private string _typeName;

	private SerializedFields _fields;

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
	
	public SerializedInfo(ISerializableInfo obj, SerializedFields fields)
	{
		if (obj is null) throw new ArgumentNullException();
		_assemblyName = obj.GetType().Assembly.FullName;
		_typeName = obj.GetType().FullName;
		_fields = fields;
	}

	public SerializedInfo()
	{
		
	}

	public T Instantiate<T>(IReadOnlyDictionary<string, Assembly> registeredAssemblies = null)
		where T : ISerializableInfo
		=> (T)Instantiate(registeredAssemblies);
	
	public ISerializableInfo Instantiate(IReadOnlyDictionary<string, Assembly> registeredAssemblies = null)
	{
		registeredAssemblies ??= Hedgemen.RegisteredAssemblies;
		// we need a way to access assemblies because NativeAoT doesn't allow you to get assemblies via
		// Activator.CreateInstance(assemblyName, typeName). Aka, eat my ass, NativeAoT. Jk, love you
		var assembly = registeredAssemblies[_assemblyName];
		var obj = (assembly.CreateInstance(_typeName) as ISerializableInfo)!;
		obj.ReadSerializedInfo(this);
		
		return obj;
	}
}