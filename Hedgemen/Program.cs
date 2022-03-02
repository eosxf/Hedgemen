using Hgm.Modding;
using Hgm.Register;

namespace Hgm;

public static class Program
{
	private static void Main(string[] args)
	{
		var initStep = new HedgemenInitStep
		{
			Proc = new HedgemenProc(),
			KazeInitStep = new KazeInitStep
			{
				Registry = new KazeRegistry()
			}
		};
		
		Hedgemen.InitializeHedgemen(initStep);
		Hedgemen.Proc.Run();
		Hedgemen.Finish();
	}
}