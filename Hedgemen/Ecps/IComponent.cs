namespace Hgm.Ecps;

public interface IComponent : IEventPropagator
{
	public Entity Self { get; set; }

	public IEventPropagator Propagator => this;

	public void InitializeComponent();

	public void PropertyRemoved(IProperty property);
}

public static class ComponentHelper
{
	public static void Initialize(this IComponent component, Entity entity)
	{
		component.Self = entity;
		component.InitializeComponent();
	}
}