using System;
using System.Diagnostics;
using System.Threading;

namespace Hgm
{
	public static class Program
	{
		private static void Main(string[] args)
		{
			Hedgemen.Proc = new HedgemenProc();
			Hedgemen.Proc.Run();
			Hedgemen.Finish();
		}
	}
}