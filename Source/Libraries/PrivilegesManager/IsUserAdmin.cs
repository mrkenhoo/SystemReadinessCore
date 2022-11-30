using System;
using System.Security.Principal;

namespace SystemReadinessCore.Management.PrivilegesManager
{
    public static class GetPrivileges
    {
        public static bool IsUserAdmin()
        {
            try
            {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
