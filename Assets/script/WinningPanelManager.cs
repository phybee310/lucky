using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinningPanelManager : MonoBehaviour
{
    public GameObject winningPanel;
    public GameObject levelFailPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeUsedText;
    public TextMeshProUGUI failscoreText;
    public TextMeshProUGUI failtimeUsedText;
    public int scoreThreshold = 50;
    public CountdownTimer countdownTimer;
    public TrashCan trashCan;

    private int itemsInTrashCan = 0;
    private bool isGameEnded = false;

    private void Start()
    {
        winningPanel?.SetActive(false);
        levelFailPanel?.SetActive(false);
    }

    public void OnItemPlacedInTrashCan()
    {
        if (isGameEnded) return;

        itemsInTrashCan++;
        if (itemsInTrashCan >= 10)
        {
            CheckWinCondition();
        }
    }

    public void CheckWinCondition()
    {
        if (isGameEnded) return;

        isGameEnded = true;
        countdownTimer.StopTimer();


        int playerScore = TrashCan.globalScore;
        float timeUsed = countdownTimer.GetElapsedTime();

        if (playerScore >= scoreThreshold)
        {
            ShowWinningPanel(playerScore, timeUsed);
        }
        else
        {
            ShowLevelFailPanel(playerScore, timeUsed);
        }
    }

    private void ShowWinningPanel(int score, float timeUsed)
    {
        winningPanel?.SetActive(true);
        scoreText.text = $"Score: {score}";
        timeUsedText.text = $"Time Used: {FormatTime(timeUsed)}";
        Debug.Log("Winning panel displayed.");

        int currentLevel = LevelManager.GetCurrentLevel();
        ScoreManager.SaveScoreAndTime(currentLevel, score, timeUsed);
        TrashCan.ResetScore();
    }


    private void ShowLevelFailPanel(int score, float timeUsed)
    {
        levelFailPanel?.SetActive(true);
        failscoreText.text = $"Score: {score}";
        failtimeUsedText.text = $"Time Used: {FormatTime(timeUsed)}";
        Debug.Log("Game over panel displayed. Player did not meet the score requirement.");
    }

    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return $"{minutes:00}:{seconds:00}";
    }
}
