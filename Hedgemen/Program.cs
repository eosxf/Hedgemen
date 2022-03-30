using System;
using Hgm.IO;
using Hgm.Modding;
using Hgm.Utilities;

namespace Hgm;

public static class Program
{
	private static void Main(string[] args)
	{
		var initStep = new HedgemenInitStep
		{
			Backend = new HedgemenBackend(),
			KazeInitStep = new KazeInitStep
			{
				Registry = new KazeRegistry()
			},
			Logger = new DefaultLogger()
		};

		try
		{
			Hedgemen.InitializeHedgemen(initStep);
			Hedgemen.Backend.Run();
			IFile hedgemenLogOutputFile = new File("hedgemen.log");
			hedgemenLogOutputFile.WriteString(Hedgemen.Logger.ToString());
			Hedgemen.Finish();
		}

		catch (Exception e)
		{
			// this might cause issues where the IFile.WriteString method is the method causing the exception to
			// be thrown, but I'm lazy
			IFile hedgemenLogOutputFile = new File("hedgemen.log");
			hedgemenLogOutputFile.WriteString(Hedgemen.Logger.ToString());
			throw;
		}
	}
}