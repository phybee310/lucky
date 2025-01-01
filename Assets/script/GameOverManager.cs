using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel; // Reference to the Game Over panel
    public static int globalScore = 0;
    [SerializeField] private GameOverManager gameOverManager; // Reference to GameOverManager

    void Start()
    {
        // Ensure the Game Over panel is hidden at the start
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    // Call this method when the game is over
    public void ShowGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    // Restart the current level
    public void RestartGame()
    {
        TrashCan.ResetScore();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Time.timeScale = 1;
    }

    // Return to the main menu
    public void GoToHome()
    {
        SceneManager.LoadScene("MainMenu"); // Replace with your actual main menu scene name
        Time.timeScale = 1;
    }
}
