using Hgm.Register;
using Hgm.Utilities;

namespace Hgm.Modding;

/// <summary>
/// Central class for Hedgemen's modding API.
/// </summary>
public class Kaze
{
	public KazeRegistry Registry { get; }
	
	public Kaze(KazeInitStep init)
	{
		Registry = init.Registry;
	}
}

public struct KazeInitStep : IInitStep
{
	public KazeRegistry Registry;

	public bool IsProperlyInitialized() => Registry != null;
}