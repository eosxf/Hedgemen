using System;
using System.Diagnostics;
using System.Threading;

namespace Hgm
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            Global.InitializeGlobal(new Hedgemen());
            Global.GetHedgemen().Run();
            Global.FinalizeGlobal();
        }
    }
}