namespace Depressurizer
{
    /// <summary>
    /// Listing of the different ways to find out about a game.
    /// The higher values take precedence over the lower values. If a game already exists with a PackageFree type, it cannot change to SteamConfig.
    /// </summary>
    public enum GameListingSource
    {
        Unknown,
        SteamConfig,
        WebProfile,
        PackageFree,
        PackageNormal,
        Manual
    }
}