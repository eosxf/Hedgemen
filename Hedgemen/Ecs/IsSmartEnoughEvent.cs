namespace Hgm.Ecs;

public class IsSmartEnoughEvent : GameEvent
{
    public int RequiredIntelligence { get; private set; }
    public bool Evalutation { get; set; } = false;
    
    public IsSmartEnoughEvent(int requiredIntelligence)
    {
        RequiredIntelligence = requiredIntelligence;
    }
}