// ReSharper disable InconsistentNaming

using HarmonyLib;

namespace DvJobPickerRedux.Patches;

[HarmonyPatch(typeof(StationProceduralJobsRuleset), "Awake")]
internal class StationProceduralJobsRulesetAwakePatch
{
    public static void Postfix(StationProceduralJobsRuleset __instance)
    {
        Main.DebugLog($"Setting Unload Shunting Jobs: {Main.Settings.UnloadShuntJobs}");
        __instance.unloadStartingJobSupported = Main.Settings.UnloadShuntJobs;

        Main.DebugLog($"Setting Load Shunting Jobs: {Main.Settings.LoadShuntJobs}");
        __instance.loadStartingJobSupported = Main.Settings.LoadShuntJobs;

        Main.DebugLog($"Setting Freight Jobs: {Main.Settings.FreightJobs}");
        __instance.haulStartingJobSupported = Main.Settings.FreightJobs;

        Main.DebugLog($"Setting Logistic Jobs: {Main.Settings.LogisticJobs}");
        __instance.emptyHaulStartingJobSupported = Main.Settings.LogisticJobs;

        Main.DebugLog($"Setting Minimum Car Length: {Main.Settings.MinimumCars}");
        __instance.minCarsPerJob = Main.Settings.MinimumCars;

        Main.DebugLog($"Setting Maximum Car Length: {Main.Settings.MinimumCars}");
        __instance.maxCarsPerJob = Main.Settings.MaximumCars;

        __instance.maxShuntingStorageTracks = Main.Settings.UnloadShuntJobs || Main.Settings.LoadShuntJobs ? 3 : 0;
    }
}