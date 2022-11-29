using System.Security.Principal;

namespace SystemReadinessCore.Source.Management.PrivilegesManager
{
    public static class GetPrivileges
    {
        public static bool IsUserAdmin()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
