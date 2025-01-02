using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private const string ScoreKeyPrefix = "LevelScore_";
    private const string TimeKeyPrefix = "LevelTime_";

    // Save the score and time for a specific level
    public static void SaveScoreAndTime(int level, int score, float timeUsed)
    {
        PlayerPrefs.SetInt(ScoreKeyPrefix + level, score);
        PlayerPrefs.SetFloat(TimeKeyPrefix + level, timeUsed);
        PlayerPrefs.Save();
        Debug.Log($"Score and time for Level {level} saved: Score = {score}, Time = {timeUsed}");
    }

    // Retrieve the score for a specific level
    public static int GetScore(int level)
    {
        return PlayerPrefs.GetInt(ScoreKeyPrefix + level, 0); // Default to 0 if no score is saved
    }

    // Retrieve the time used for a specific level
    public static float GetTime(int level)
    {
        return PlayerPrefs.GetFloat(TimeKeyPrefix + level, 0f); // Default to 0 if no time is saved
    }

    // Clear all scores and times (optional for debugging or resetting game progress)
    public static void ClearScoresAndTimes()
    {
        for (int level = 1; level <= 3; level++) // Adjust range based on total levels
        {
            PlayerPrefs.DeleteKey(ScoreKeyPrefix + level);
            PlayerPrefs.DeleteKey(TimeKeyPrefix + level);
        }
        PlayerPrefs.Save();
        Debug.Log("All scores and times cleared.");
    }
}
