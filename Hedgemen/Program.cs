using System;
using System.IO;
using Hgm.IO;
using Hgm.Modding;
using Hgm.Register;
using Hgm.Utilities;
using File = Hgm.IO.File;

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
			},
			Logger = new DefaultLogger()
		};

		try
		{
			Hedgemen.InitializeHedgemen(initStep);
			Hedgemen.Proc.Run();
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