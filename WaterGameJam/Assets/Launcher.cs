using System.Diagnostics;
using System.IO;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [Header("Launcher Settings")]
    public string launcherExeName = "Launcher.exe";

    public void GoBackToLauncher()
    {
        // Get current game's root folder
        string rootPath = Directory.GetParent(Application.dataPath).FullName;

        // Move up one more level (because games are inside folders like /GameA/)
        string parentPath = Directory.GetParent(rootPath).FullName;

        // Build launcher path
        string launcherPath = Path.Combine(parentPath, launcherExeName);

        UnityEngine.Debug.Log("Returning to launcher: " + launcherPath);

        if (File.Exists(launcherPath))
        {
            try
            {
                Process.Start(launcherPath);
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogError("❌ Failed to start launcher: " + e.Message);
                return;
            }

            // Close current game
            Application.Quit();
        }
        else
        {
            UnityEngine.Debug.LogError("❌ Launcher not found at: " + launcherPath);
        }
    }
}