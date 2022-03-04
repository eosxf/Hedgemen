using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Hgm.Ecs.Text;
using Hgm.IO.Serialization;
using Hgm.Register;
using Hgm.Utilities;

namespace Hgm.Ecs;

/// <summary>
/// Component class for <see cref="Hgm.Ecs.Entity" />
/// </summary>
public abstract class Component : IComponent
{
	private readonly IDictionary<Type, ComponentEventWrapper> _registeredEvents = new Dictionary<Type, ComponentEventWrapper>();
	public Entity Self { get; private set; }

	private bool _initialized = false;

	public void Initialize()
	{
		ThrowIfInitialized();
		_initialized = true;
		InitializeSelf();
	}

	public void Initialize(IHasSerializedFields handle)
	{
		ThrowIfInitialized();
		_initialized = true;
		InitializeSelf();
		InitializeFromFields(handle, handle.Fields);
	}

	private void ThrowIfInitialized()
	{
		if (_initialized)
			throw new InvalidOperationException($"Component '{GetType()}' is already initialized.");
	}

	protected abstract void InitializeSelf();

	/// <summary>
	/// Initializes from fields.
	/// </summary>
	/// <param name="handle">The owner of the fields parameter. Useful for type matching for initialization
	/// context.</param>
	/// <param name="fields">The serialized fields to read from. Owner is the handle parameter</param>
	protected virtual void InitializeFromFields(IHasSerializedFields handle, SerializedFields fields)
	{
		
	}

	public bool IsActive { get; set; } = true;

	public bool HandleEvent(GameEvent e)
	{
		if (!_registeredEvents.ContainsKey(e.GetType())) return false;
		_registeredEvents[e.GetType()](e);
		OnEventPropagated(e);
		return true;
	}

	public void RegisterEvent<TEvent>(ComponentEvent<TEvent> e) where TEvent : GameEvent
	{
		if (e == null)
			throw new Exception("Registered events cannot be null!");

		if (_registeredEvents.ContainsKey(typeof(TEvent)))
			throw new Exception($"Event type {typeof(TEvent).FullName} already registered.");

		void ComponentEvent(GameEvent handle)
		{
			e(handle as TEvent);
		}

		_registeredEvents.Add(typeof(TEvent), ComponentEvent);
	}

	// for now QueryComponentInfo will remain abstract, might become virtual eventually
	public abstract ComponentInfo QueryComponentInfo();

	private NamespacedString QueryDefaultRegistryName()
	{
		string assemblyName = GetType().Assembly.GetName().Name!;
		string typeName = ConvertTypeNameToSnakeCase();

		return new NamespacedString
			(assemblyName.ToLower(CultureInfo.InvariantCulture),
			 typeName);
	}

	private string ConvertTypeNameToSnakeCase()
	{
		string typeName = GetType().Name;
		var uppercaseOccurrences = typeName.AllUppercaseInstances();
		var builder = new StringBuilder(typeName.Length + uppercaseOccurrences.Count);
		builder.Append(typeName);
		
		foreach (var occurrence in uppercaseOccurrences)
			builder.Replace(occurrence.ToString(), "_" + char.ToLowerInvariant(occurrence));
		
		// remove the likely char[0] _ char with string.Empty
		// since classes should be PascalCase and thus would create _
		if (builder[0] == '_')
			builder.Replace("_", string.Empty, 0, 1);
		
		return builder.ToString();
	}

	public virtual SerializedInfo GetSerializedInfo()
	{
		return new SerializedInfo(this, new SerializedFields());
	}

	public virtual void ReadSerializedInfo(SerializedInfo info)
	{
		InitializeFromFields(info, info.Fields);
	}

	public bool IsEventRegistered<TEvent>() where TEvent : GameEvent
	{
		return IsEventRegistered(typeof(TEvent));
	}

	public bool IsEventRegistered(Type eventType)
	{
		return _registeredEvents.ContainsKey(eventType);
	}

	public void AttachEntity(Entity entity)
	{
		if (Self != null)
			throw new InvalidOperationException($"An entity is already attached to component: {this}");
		Self = entity;
	}

	public virtual void OnEventPropagated(GameEvent gameEvent)
	{
		
	}
}