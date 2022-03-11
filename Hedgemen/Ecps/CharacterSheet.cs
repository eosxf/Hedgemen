using System;

namespace Hgm.Ecps;

public class CharacterSheet : IComponent
{
	public Entity Self { get; set; }

	public CharacterStats Stats { get; private set; }
	
	public void InitializeComponent()
	{
		Self.EnsureProperty<CharacterStats>();
		Stats = Self.GetProperty<CharacterStats>();
	}

	public void PropertyRemoved(IProperty property)
	{
		if (property is CharacterStats)
			Stats = null;
	}

	public bool Propagate(Event gameEvent)
	{
		switch (gameEvent)
		{
			case ChangeStrengthEvent e: ChangeStrength(e);
				return true;
		}

		return false;
	}

	public void ChangeStrength(ChangeStrengthEvent e)
	{
		//Console.WriteLine($"Changed strength from {Stats.Strength} to {e.NewValue}");
		Stats.Strength = e.NewValue;
	}
}