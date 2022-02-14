using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Hgm.Ecs.Serialization;

[Serializable]
public class SerializedInfo : IEnumerable<string>
{
	private string _assemblyName;
	private string _typeName;

	private Dictionary<string, object> _fields = new();

	public SerializedInfo(ISerializableInfo obj)
	{
		if (obj is null) throw new ArgumentNullException();
		_assemblyName = obj.GetType().Assembly.FullName;
		_typeName = obj.GetType().FullName;
	}

	public T ConstructObject<T>() where T : ISerializableInfo, new()
	{
		var obj = new T();
		obj.ReadSerializedInfo(this);
		return obj;
	}
	
	public ISerializableInfo ConstructObject(IReadOnlyDictionary<string, Assembly> registeredAssemblies)
	{
		// we need a way to access assemblies because NativeAoT doesn't allow you to get assemblies via
		// Activator.CreateInstance(assemblyName, typeName). Aka, eat my ass, NativeAoT. Jk, love you
		var assembly = registeredAssemblies[_assemblyName];
		var obj = assembly.CreateInstance(_typeName) as ISerializableInfo;
		obj.ReadSerializedInfo(this);
		
		return obj;
	}

	public void Add(string fieldName, object field)
	{
		_fields.Add(fieldName, field);
	}

	public T Get<T>(string fieldName)
	{
		return (T)_fields[fieldName];
	}

	public IEnumerator<string> GetEnumerator()
	{
		return _fields.Keys.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}