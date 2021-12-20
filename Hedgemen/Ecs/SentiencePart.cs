using System;

namespace Hgm.Ecs;

public class SentiencePart : Part, ISentience
{
    public SentiencePart()
    {
        RegisterEvent<IsSmartEnoughEvent>(IsSmartEnough);
    }

    public override ComponentInfo QueryComponentInfo()
    {
        return new ComponentInfo
        {
            RegistryName = "hedgemen:sentience_part",
            AccessType = typeof(ISentience)
        };
    }

    public void IsSmartEnough(IsSmartEnoughEvent e)
    {
        e.Evalutation = Intelligence >= e.RequiredIntelligence;
    }

    public int Intelligence { get; set; } = new Random().Next(0, 20);
}