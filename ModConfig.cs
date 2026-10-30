namespace CompressedHorse;

/// <summary>Player settings, saved to config.json.</summary>
internal sealed class ModConfig
{
    /// <summary>Whether players walk through parked horses instead of being blocked by them.</summary>
    public bool PlayersPassThroughHorses { get; set; }
}
