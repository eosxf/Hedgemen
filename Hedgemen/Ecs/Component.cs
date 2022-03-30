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
///	Component class for <see cref="Hgm.Ecs.Entity" />
/// </summary>
public abstract class Component : ISerializableState
{
	private readonly IDictionary<Type, ComponentEventWrapper> _registeredEvents =
		new Dictionary<Type, ComponentEventWrapper>();

	private bool _initialized;

	public Entity Self { get; private set; }

	public bool IsActive { get; set; } = true;

	public virtual SerializationState GetObjectState()
	{
		return new SerializationState(this);
	}

	public virtual void SetObjectState(SerializationState state)
	{
	}

	public void Initialize()
	{
		ThrowIfInitialized();
		_initialized = true;
		InitializeComponent();
	}

	internal void ReadFromSerializationState(SerializationState state)
	{
		ThrowIfInitialized();
		_initialized = true;
		InitializeComponent();
		SetObjectState(state);
	}

	internal void ReadFromComponentSchema(ComponentSchema schema)
	{
		ThrowIfInitialized();
		_initialized = true;
		InitializeComponent();
		ReadComponentSchema(schema);
	}

	protected virtual void ReadComponentSchema(ComponentSchema schema)
	{
	}

	private void ThrowIfInitialized()
	{
		if (_initialized)
			throw new InvalidOperationException($"Component '{GetType()}' is already initialized.");
	}

	protected abstract void InitializeComponent();

	public bool HandleEvent(GameEvent e)
	{
		if (!_registeredEvents.ContainsKey(e.GetType())) return false;
		_registeredEvents[e.GetType()](e);
		OnEventPropagated(e);
		return true;
	}

	protected void RegisterEvent<TEvent>(ComponentEvent<TEvent> e) where TEvent : GameEvent
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

		return new NamespacedString(
			assemblyName.ToLower(CultureInfo.InvariantCulture),
			typeName);
	}

	private string ConvertTypeNameToSnakeCase()
	{
		string typeName = GetType().Name;
		var uppercaseOccurrences = typeName.AllUppercaseInstances();
		var builder = new StringBuilder(typeName.Length + uppercaseOccurrences.Count);
		builder.Append(typeName);

		foreach (char occurrence in uppercaseOccurrences)
			builder.Replace(occurrence.ToString(), "_" + char.ToLowerInvariant(occurrence));

		// remove the likely char[0] _ char with string.Empty
		// since classes should be PascalCase and thus would create _
		if (builder[0] == '_')
			builder.Replace("_", string.Empty, 0, 1);

		return builder.ToString();
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