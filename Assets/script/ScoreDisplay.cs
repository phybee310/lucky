using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the TextMeshPro component for displaying total score

    void Update()
    {
        // Update the total score text with the current global score
        if (scoreText != null)
        {
            scoreText.text = $"Total Score: {TrashCan.globalScore}"; // Accessing the static globalScore
        }
        else
        {
            Debug.LogWarning("Score Text is not assigned!");
        }
    }
}
