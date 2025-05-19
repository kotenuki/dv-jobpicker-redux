// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Linq;
using DV.Logic.Job;
using DvJobPickerRedux.Managers;
using HarmonyLib;

namespace DvJobPickerRedux.Patches;

[HarmonyPatch(typeof(Yard))]
[HarmonyPatch(MethodType.Constructor)]
[HarmonyPatch([typeof(List<Track>), typeof(List<Track>), typeof(List<Track>), typeof(List<WarehouseMachine>), typeof(string)])]
internal class YardPatch
{
    public static void Prefix(Yard __instance, ref List<Track> storageTracks, ref List<Track> transferOutTracks, ref string stationID)
    {
        var id = stationID;

        Main.DebugLog($"Preparing Yard Patch for {id}...");

        if (Main.Settings.UsePassengerTracks)
        {
            Main.DebugLog($"Using Passenger Tracks for {id}...");

            if (Main.Settings.LiveDangerously)
            {
                Main.DebugLog($"Living Dangerously for {id}...");

                var passengerLoadingTracksIds = TrackManager.Tracks.Loading
                    .SingleOrDefault(x => x.YardId.Equals(id, StringComparison.OrdinalIgnoreCase))?
                    .Tracks ?? [];

                if (passengerLoadingTracksIds.Any())
                {
                    var passengerLoadingTracks = passengerLoadingTracksIds.Select(trackId => RailTrackRegistry.Instance.AllTracks
                        .FirstOrDefault(x => x.LogicTrack().ID.ToString() == trackId)
                        ?.LogicTrack()).OfType<Track>().ToList();

                    Main.DebugLog($"Adding Passenger Loading to Transfer Out Tracks for {id}: {passengerLoadingTracks.Select(x => x.ID.FullDisplayID).Join()}");

                    transferOutTracks.AddRange(passengerLoadingTracks);
                }
                else
                {
                    Main.DebugLog($"No Passenger Loading tracks found for {id}, skipping...");
                }
            }

            var passengerStorageTrackIds = TrackManager.Tracks.Storage
                .SingleOrDefault(x => x.YardId.Equals(id, StringComparison.OrdinalIgnoreCase))?
                .Tracks ?? [];

            if (passengerStorageTrackIds.Any())
            {
                var passengerStorageTracks = passengerStorageTrackIds.Select(trackId => RailTrackRegistry.Instance.AllTracks
                    .FirstOrDefault(x => x.LogicTrack().ID.ToString() == trackId)
                    ?.LogicTrack()).OfType<Track>().ToList();

                Main.DebugLog($"Adding Passenger Storage to Storage Tracks for {id}: {passengerStorageTracks.Select(x => x.ID.FullDisplayID).Join()}");

                storageTracks.AddRange(passengerStorageTracks);
            }
            else
            {
                Main.DebugLog($"No Passenger Storage tracks found for {id}, skipping...");
            }
        }

        if (!Main.Settings.CombineTracks) return;

        Main.DebugLog($"Combined Transfer Out and Storage tracks for {id}...");

        storageTracks.AddRange(transferOutTracks);
        transferOutTracks = storageTracks;
    }
}