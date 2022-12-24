using System;

namespace SystemReadinessCore.Libraries.RuntimeManager
{
    public partial class Runtime
    {
        public static readonly int WindowsMajorVersion = Environment.OSVersion.Version.Major;
        public static readonly int WindowsBuildVersion = Environment.OSVersion.Version.Build;

        public static bool IsSystemCompatible()
        {
            if (WindowsMajorVersion != 10)
            {
                throw new NotSupportedException();
            }
            else if (WindowsBuildVersion < 19041)
            {
                return false;
            }
            else if (WindowsBuildVersion == 20348)
            {
                return false;
            }
            return true;
        }
    }
}
