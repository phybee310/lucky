using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel; // Reference to the Game Over panel

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
        }
    }

    // Restart the current level
    public void RestartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Time.timeScale = 1;

        // Reload the current scene
    }

    // Return to the main menu
    public void GoToHome()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
        // Load the main menu scene (replace "MainMenu" with your actual scene name)
    }
}
