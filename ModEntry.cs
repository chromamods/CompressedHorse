using HarmonyLib;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Characters;

namespace CompressedHorse;

/// <summary>Narrows the ridden horse's hitbox and optionally lets players walk through parked horses.</summary>
internal sealed class ModEntry : Mod
{
    /// <summary>The farmer's own hitbox width in pixels, so the ridden horse fits wherever the farmer does.</summary>
    private const int RiddenWidth = 48;

    private ModConfig config = null!;
    private bool passThroughApplied;

    public override void Entry(IModHelper helper)
    {
        config = helper.ReadConfig<ModConfig>();
        new Harmony(ModManifest.UniqueID).Patch(
            AccessTools.Method(typeof(Horse), nameof(Horse.GetBoundingBox)),
            postfix: new HarmonyMethod(typeof(ModEntry), nameof(GetBoundingBoxPostfix)));
        helper.Events.GameLoop.GameLaunched += OnGameLaunched;
        helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
    }

    private static void GetBoundingBoxPostfix(Horse __instance, ref Rectangle __result)
    {
        // Only once fully mounted: mounting and dismounting position the farmer and horse from the
        // vanilla box. Vanilla's gate squeeze (24px) is already narrower, so it is left alone.
        if (__instance.rider?.mount != __instance || __instance.dismounting.Value || __result.Width <= RiddenWidth)
            return;
        __result = new Rectangle(__result.X + (__result.Width - RiddenWidth) / 2, __result.Y, RiddenWidth, __result.Height);
    }

    private void OnGameLaunched(object? sender, GameLaunchedEventArgs e)
    {
        var gmcm = Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
        if (gmcm is null)
            return;
        gmcm.Register(ModManifest, reset: () => config = new ModConfig(), save: () => Helper.WriteConfig(config));
        gmcm.AddBoolOption(
            ModManifest,
            getValue: () => config.PlayersPassThroughHorses,
            setValue: value => config.PlayersPassThroughHorses = value,
            name: () => Helper.Translation.Get("config.pass-through.name"),
            tooltip: () => Helper.Translation.Get("config.pass-through.tooltip"));
    }

    private void OnUpdateTicked(object? sender, UpdateTickedEventArgs e)
    {
        if (!Context.IsWorldReady)
            return;
        if (config.PlayersPassThroughHorses)
        {
            // Vanilla clears farmerPassesThrough on mount, dismount and after a push, so it is reasserted every tick.
            foreach (NPC npc in Game1.currentLocation.characters)
            {
                if (npc is Horse)
                    npc.farmerPassesThrough = true;
            }
            passThroughApplied = true;
        }
        else if (passThroughApplied)
        {
            Utility.ForEachCharacter(npc =>
            {
                if (npc is Horse)
                    npc.farmerPassesThrough = false;
                return true;
            });
            passThroughApplied = false;
        }
    }
}
