using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Hgm.IO.Serialization;

[Serializable]
public class SerializationState
{
	private string _assemblyName;
	
	private string _typeName;

	private Dictionary<string, object> _fields = new();

	[JsonInclude]
	[JsonPropertyName("fields")]
	public IReadOnlyDictionary<string, object> Fields
	{
		get => _fields;
		private set => _fields = value as Dictionary<string, object>;
	}

	public void AddValue(string name, object val)
	{
		if (_fields.ContainsKey(name))
			throw new ArgumentException($"Key '{nameof(name)}' already exists in the serialization state");
		_fields.Add(name, val);
	}

	// todo maybe put in static method for code reuse
	public T GetValue<T>(string name, T defaultReturn = default)
	{
		if (!_fields.ContainsKey(name)) return defaultReturn;
		
		var json = _fields[name];
		var obj = defaultReturn;

		if (json is JsonObject jsonObject)
			obj = jsonObject.Deserialize<T>();

		else if (json is JsonElement jsonElement) 
			obj = jsonElement.Deserialize<T>();

		return obj;
	}

	public void GetValue<T>(string name, out T value)
	{
		value = GetValue(name, default(T));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public SerializationState GetState(string name) => GetValue<SerializationState>(name, null);

	public SerializationState(ISerializableState obj)
	{
		if (obj is null) throw new ArgumentNullException(nameof(obj));
		_assemblyName = obj.GetType().Assembly.GetName().Name;
		_typeName = obj.GetType().FullName;
	}
	
	public SerializationState(Type type)
	{
		if (type is null) throw new ArgumentNullException(nameof(type));
		_assemblyName = type.Assembly.GetName().Name;
		_typeName = type.FullName;
	}

	public SerializationState()
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

	public T Instantiate<T>(IReadOnlyDictionary<string, Assembly> registeredAssemblies = null, bool init = true)
		where T : ISerializableState
	{
		return (T) Instantiate(registeredAssemblies, init);
	}

	public ISerializableState Instantiate(IReadOnlyDictionary<string, Assembly> registeredAssemblies = null,
										 bool init = true)
	{
		registeredAssemblies ??= Hedgemen.RegisteredAssemblies;
		// we need a way to access assemblies because NativeAoT doesn't allow you to get assemblies via
		// Activator.CreateInstance(assemblyName, typeName). Aka, eat my ass, NativeAoT. Jk, love you
		var assembly = registeredAssemblies[_assemblyName];
		var obj = (assembly.CreateInstance(_typeName) as ISerializableState)!;
		if(init)
			obj.SetObjectState(this);
		return obj;
	}
}