using Hgm.Register;
using Hgm.Utilities;

namespace Hgm.Modding;

/// <summary>
///     Central class for Hedgemen's modding API.
/// </summary>
public class Kaze
{
	public Kaze(KazeInitStep init)
	{
		Registry = init.Registry;
		NewRegistry = new ObjectRegistry();
	}

	public KazeRegistry Registry { get; }

	public ObjectRegistry NewRegistry { get; } // test
}

public struct KazeInitStep : IInitStep
{
	public KazeRegistry Registry;

	public bool IsProperlyInitialized()
	{
		return Registry != null;
	}
}