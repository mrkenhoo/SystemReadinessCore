namespace SystemReadinessCore.Utilities.UpdatesManager
{
    public partial class Updater
    {
        public static bool IsUpdateAvailable()
        {
            return NewUpdateAvailable switch
            {
                0 => true,
                _ => false,
            };
        }
    }
}
