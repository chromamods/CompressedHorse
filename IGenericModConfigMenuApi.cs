using System;
using StardewModdingAPI;

namespace CompressedHorse;

/// <summary>The subset of Generic Mod Config Menu's API this mod uses (SMAPI maps it by signature).</summary>
public interface IGenericModConfigMenuApi
{
    void Register(IManifest mod, Action reset, Action save, bool titleScreenOnly = false);

    void AddBoolOption(IManifest mod, Func<bool> getValue, Action<bool> setValue, Func<string> name, Func<string>? tooltip = null, string? fieldId = null);
}
