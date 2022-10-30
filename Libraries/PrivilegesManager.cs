using System.Security.Principal;

namespace SystemReadinessCore.Libraries
{
    public class PrivilegesManager
    {
        public static bool IsUserAdmin()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
