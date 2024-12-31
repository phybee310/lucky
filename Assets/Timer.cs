using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float startTime = 60f; // Set the countdown start time in seconds
    public TextMeshProUGUI timerText; // Reference to the TextMeshProUGUI component
    public GameObject gameOverPanel; // Reference to the Game Over panel

    private float currentTime;   // Keeps track of the remaining time
    private bool isTimerRunning; // Flag to control the timer

    void Start()
    {
        currentTime = startTime;
        isTimerRunning = true;

        // Ensure the Game Over panel is hidden initially
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        UpdateTimerUI();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                currentTime = 0;
                isTimerRunning = false;
                TimerEnded();
            }

            UpdateTimerUI();
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimerEnded()
    {
        Debug.Log("Timer has ended!");

        // Show the Game Over panel
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
    }
}
