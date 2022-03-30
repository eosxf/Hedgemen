using Hgm.Ecs.Text;
using Hgm.EcsNew.Systems;
using Hgm.IO.Serialization;

namespace Hgm.EcsNew;

public class CharacterAttributes : IComponent
{
	public Entity Self { get; private set; }

	public int Strength = 10;
	public int Dexterity = 10;
	public int Constitution = 10;
	public int Intelligence = 10;
	public int Wisdom = 10;
	public int Charisma = 10;
	
	public void Initialize(Entity self)
	{
		Self = self;
		Self.RegisterEvent<ModifyStatEvent>(this, ModifyStat);
	}

	public void ModifyStat(ModifyStatEvent e)
	{
		switch (e.StatName)
		{
			case "strength": Strength = e.Value;
				break;
		}
	}
	
	public SerializationState GetObjectState()
	{
		var state = new SerializationState(this);
		state.AddValue("strength", Strength);
		state.AddValue("dexterity", Dexterity);
		state.AddValue("constitution", Constitution);
		state.AddValue("intelligence", Intelligence);
		state.AddValue("wisdom", Wisdom);
		state.AddValue("charisma", Charisma);
		return state;
	}

	public void SetObjectState(SerializationState state)
	{
		Strength = state.GetValue("strength", Strength);
		Dexterity = state.GetValue("dexterity", Dexterity);
		Constitution = state.GetValue("constitution", Constitution);
		Intelligence = state.GetValue("intelligence", Intelligence);
		Wisdom = state.GetValue("wisdom", Wisdom);
		Charisma = state.GetValue("charisma", Charisma);
	}

	public void ReadComponentSchema(ComponentSchema schema)
	{
		
	}

	public ComponentInfoQuery QueryComponentInfo()
	{
		return new ComponentInfoQuery
		{
			RegistryName = "hedgemen:character_attributes",
			SystemInteractions =
			{
				typeof(MovementSystem)
			}
		};
	}
}