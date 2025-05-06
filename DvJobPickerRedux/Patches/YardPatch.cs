// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using DV.Logic.Job;
using HarmonyLib;

namespace DvJobPickerRedux.Patches;

[HarmonyPatch(typeof(Yard))]
[HarmonyPatch(MethodType.Constructor)]
[HarmonyPatch([typeof(List<Track>), typeof(List<Track>), typeof(List<Track>), typeof(List<WarehouseMachine>), typeof(string)])]
internal class YardPatch
{
    public static void Prefix(Yard __instance, ref List<Track> storageTracks, ref List<Track> transferOutTracks)
    {
        Main.DebugLog(() => "Preparing Yard Patch...");

        if (!Main.Settings.CombineTracks)
        {
            Main.DebugLog(() => "Combine Tracks was not set, skipping...");

            return;
        }

        Main.DebugLog(() => "Combined Transfer and Storage tracks.");

        storageTracks.AddRange(transferOutTracks);

        transferOutTracks = storageTracks;
    }
}