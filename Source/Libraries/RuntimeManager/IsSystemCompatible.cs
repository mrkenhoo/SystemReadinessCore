using System;

namespace SystemReadinessCore.Utilities.RuntimeHandler
{
    public partial class Runtime
    {
        public static bool IsSystemCompatible()
        {
            if (Environment.OSVersion.Version.Build < 19041)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
