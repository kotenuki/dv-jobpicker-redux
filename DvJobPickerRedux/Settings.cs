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

    [Draw("Minimum Cars?", Min = 3, Max = 10, Tooltip = "The minimum car amount per job. Warning, the higher this number the fewer jobs will spawn. Recommended: 3")]
    public int MinimumCars = 3;

    [Draw("Maximum Cars?", Min = 6, Max = 20, Tooltip = "The maximum car amount per job. Recommended: 20")]
    public int MaximumCars = 20;

    [Draw("Use both Output and Storage Tracks?", Tooltip = "Use both output and storage tracks for jobs.")]
    public bool CombineTracks = true;

    [Draw("Use Passenger Tracks?", Tooltip = "Use passenger storage tracks for jobs.")]
    public bool UsePassengerTracks = false;

    [Draw("Live Dangerously?", VisibleOn = "UsePassengerTracks|true",
        Tooltip = "Use passenger loading tracks for jobs. Warning, this can and will spawn jobs in loading lanes, which at some stations can be detrimental to your health.")]
    public bool LiveDangerously = false;

    [Draw("Enable Debug Logging?")]
    public bool EnableLogging = false;

    public override void Save(UnityModManager.ModEntry entry)
    {
        Save(this, entry);
    }

    public void OnChange() { }
}