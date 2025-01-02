using UnityEngine;
using TMPro;

public class ScoreViewer : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the TextMeshPro UI element

    private void Start()
    {
        DisplayScoresAndTimes();
    }

    public void DisplayScoresAndTimes()
    {
        if (scoreText == null)
        {
            Debug.LogError("ScoreText is not assigned!");
            return;
        }

        string displayText = "Level Scores and Times:\n";

        for (int level = 1; level <= 3; level++) // Adjust range based on total levels
        {
            int score = ScoreManager.GetScore(level);
            float timeUsed = ScoreManager.GetTime(level);
            displayText += $"Level {level}: Score = {score}, Time = {FormatTime(timeUsed)}\n";
        }

        scoreText.text = displayText;
    }

    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return $"{minutes:00}:{seconds:00}";
    }
}
