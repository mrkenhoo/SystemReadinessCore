using System;

namespace SystemReadinessCore.Utilities.RuntimeHandler
{
    public partial class Runtime
    {
        public static bool IsSystemCompatible()
        {
            switch (Environment.OSVersion.Version.Build < 19041)
            {
                case true:
                    return true;
                case false:
                    return false;
            }
        }
    }
}
