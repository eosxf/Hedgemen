namespace Hgm
{
    public static class Global
    {
        private static IHedgemen hedgemen;

        public static IHedgemen GetHedgemen() => hedgemen;

        public static void InitializeGlobal(IHedgemen hedgemen)
        {
            Global.hedgemen = hedgemen;
        }

        public static void FinalizeGlobal()
        {
            hedgemen.Exit();
            hedgemen.Dispose();
            hedgemen = null;
        }
    }
}