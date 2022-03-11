using Hgm.Ecs.Text;
using Hgm.IO.Serialization;

namespace Hgm.Ecs;

public abstract class CharacterRace : Component
{
	public string RaceName { get; set; }
	
	protected override void InitializeSelf()
	{
		RaceName = "none";
	}

	public override ComponentInfo QueryComponentInfo()
	{
		return new ComponentInfo
		{
			RegistryName = "any:character_race",
			AccessType = GetType(),
			PropagatesIgnoredEvents = false
		};
	}

	protected override void InitializeFromSchema(ComponentSchema schema)
	{
		RaceName = schema.GetValue("race_name", "none");
	}

	public override SerializationState GetObjectState()
	{
		var state = new SerializationState(this);
		state.AddValue("race_name", RaceName);
		return state;
	}

	public override void SetObjectState(SerializationState state)
	{
		RaceName = state.GetValue("race_name", "none");
	}
}