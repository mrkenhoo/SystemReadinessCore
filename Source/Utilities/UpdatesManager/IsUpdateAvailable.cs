namespace SystemReadinessCore.Libraries.UpdatesManager
{
    public partial class Updater
    {
        public static bool IsUpdateAvailable()
        {
            if (NewUpdateAvailable < 0)
            {
                return true;
            }
            else if (NewUpdateAvailable > 0)
            {
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}
