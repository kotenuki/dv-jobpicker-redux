using UnityEngine;
using UnityModManagerNet;

namespace DvJobPickerRedux;

public class Settings : UnityModManager.ModSettings, IDrawable
{
    public readonly string? Version = Main.Mod?.Info.Version;

    [Header("Settings will only apply on save re-load.")]
    [Draw("Shunting (Load) Jobs?", Tooltip = "Allow shunting (load) jobs to generate.")]
    public bool LoadShuntJobs = true;

    [Draw("Shunting (Unload) Jobs?", Tooltip = "Allow shunting (unload) jobs to generate.")]
    public bool UnloadShuntJobs = true;

    [Draw("Freight Jobs?", Tooltip = "Allow freight jobs to generate.")]
    public bool FreightJobs = true;

    [Draw("Logistics Jobs?", Tooltip = "Allow logistic jobs to generate.")]
    public bool LogisticJobs = true;

    [Draw("Minimum Cars?", Min = 1d, Max = 19d, Tooltip = "The minimum car amount per job. Recommended: 5")]
    public int MinimumCars = 3;

    [Draw("Use both Output and Storage Tracks?", Tooltip = "Use both output and storage tracks for jobs.")]
    public bool CombineTracks = true;

    [Draw("Enable Debug Logging?")]
    public bool EnableLogging = false;


    public override void Save(UnityModManager.ModEntry entry)
    {
        Save(this, entry);
    }

    public void OnChange() { }
}