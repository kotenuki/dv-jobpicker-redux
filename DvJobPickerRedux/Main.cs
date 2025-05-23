﻿using System;
using DvJobPickerRedux.Managers;
using HarmonyLib;
using UnityModManagerNet;

namespace DvJobPickerRedux;

#if DEBUG
[EnableReloading]
#endif
internal class Main
{
    public static UnityModManager.ModEntry? Mod;
    public static Settings Settings = new();

    public static void DebugLog(string message)
    {
        if (Settings.EnableLogging) Mod?.Logger.Log(message);
    }

    public static bool Load(UnityModManager.ModEntry modEntry)
    {
        Mod = modEntry;

        try
        {
            var loaded = UnityModManager.ModSettings.Load<Settings>(modEntry);
            Settings = loaded.Version == Mod.Info.Version ? loaded : new Settings();

            TrackManager.LoadTrackConfiguration();
        }
        catch
        {
            Settings = new Settings();
        }

        Mod.OnGUI = OnGUI;
        Mod.OnSaveGUI = OnSaveGUI;
        Mod.OnToggle = OnToggle;


        return true;
    }

    private static void OnGUI(UnityModManager.ModEntry modEntry)
    {
        Settings.Draw(modEntry);
    }

    private static void OnSaveGUI(UnityModManager.ModEntry modEntry)
    {
        Settings.Save(modEntry);
    }

    private static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
    {
        try
        {
            var harmony = new Harmony(modEntry.Info.Id);

            if (value)
            {
                harmony.PatchAll();
                return true;
            }

            harmony.UnpatchAll(modEntry.Info.Id);
            return true;
        }
        catch (Exception ex)
        {
            modEntry.Logger.LogException($"Failed to load {modEntry.Info.DisplayName}:", ex);
            return false;
        }
    }
}