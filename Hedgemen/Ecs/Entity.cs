using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Hgm.IO.Serialization;
using Hgm.Utilities;

namespace Hgm.Ecs;

public delegate void PartEvent(GameEvent e);

public class Entity : IEntity
{
	private IDictionary<Type, Part> _parts = new CachedDictionary<Type, Part>();

	public void AddPart(Part part)
	{
		var infoQuery = part.QueryComponentInfo();

		if (_parts.ContainsKey(infoQuery.AccessType))
			throw new ArgumentException($"{GetType().Name} already has a {nameof(Part)} with an AccessType of " +
						                    $"{infoQuery.AccessType.FullName}");
		
		_parts.Add(infoQuery.AccessType, part);
	}

	public T GetPart<T>() where T : class
	{
		return _parts.Get(typeof(T)) as T;
	}

	public TEvent Propagate<TEvent>(TEvent e) where TEvent : GameEvent
	{
		foreach(var part in _parts.Values)
		{
			if (!part.IsActive) continue;
			
			var info = part.QueryComponentInfo();
			
			if (part.IsEventRegistered<TEvent>())
			{
				bool result = part.HandleEvent(e);
				if (result) e.Handled = true;
			}
			
			else if (info.PropagatesIgnoredEvents)
			{
				part.OnEventPropagated(e);
			}
		}
		
		return e;
	}

	public bool HasPart<T>() where T : class
	{
		return _parts.ContainsKey(typeof(T));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool WillRespondTo(GameEvent e)
	{
		return WillRespondTo(e.GetType());
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool WillRespondTo<TEvent>() where TEvent : GameEvent
	{
		return WillRespondTo(typeof(TEvent));
	}

	public bool WillRespondTo(Type eventType)
	{
		foreach(var part in _parts.Values)
		{
			if(part.IsEventRegistered(eventType))
				return true;
		}

		return false;
	}

	public SerializedInfo GetSerializedInfo()
	{
		var fields = new SerializedFields();
		
		foreach (var part in _parts.Values)
		{
			fields.Add(part.QueryComponentInfo().RegistryName, part.GetSerializedInfo());
		}

		return new SerializedInfo(this, fields);
	}

	public void ReadSerializedInfo(SerializedInfo info)
	{
		foreach (var partName in info.Fields)
		{
			var partSerializedInfo = info.Fields.Get<SerializedInfo>(partName.Key);
			var part = partSerializedInfo.Instantiate<Part>(Global.GetHedgemen().Globals.RegisteredAssemblies);
			var partInfo = part.QueryComponentInfo();
			_parts.Add(partInfo.AccessType, part);
		}
	}
}