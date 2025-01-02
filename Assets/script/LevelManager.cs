using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private const string CurrentLevelKey = "CurrentLevel";

    [Header("Level Setup")]
    public int levelNumber; // Set this in the Inspector for each scene

    private void Start()
    {
        // Set the current level when the scene starts
        SetCurrentLevel(levelNumber);
    }

    // Save the current level to PlayerPrefs
    public static void SetCurrentLevel(int level)
    {
        PlayerPrefs.SetInt(CurrentLevelKey, level);
        PlayerPrefs.Save();
        Debug.Log($"Current level set to {level}");
    }

    // Retrieve the current level from PlayerPrefs
    public static int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt(CurrentLevelKey, 1); // Default to level 1 if not set
    }
}
