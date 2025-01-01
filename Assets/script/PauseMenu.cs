
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    // Reference to global score (assuming globalScore is static in TrashCan or another class)
    public static int globalScore = 0;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0; // Freeze time during pause
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1; // Resume time when game is unpaused
    }

    public void Restart()
    {
        // Reset global score when restarting the game
        TrashCan.ResetScore();

        // Reload the current scene to restart the game
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

        Time.timeScale = 1; // Ensure time is resumed after restart
    }

    public void Home()
    {
        // Load the main menu scene
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1; // Ensure time is resumed when going to main menu
    }

    public void QuitGame()
    {
        // Optionally, load a specific level or quit to desktop
        SceneManager.LoadScene("game level");
        Time.timeScale = 1; // Ensure time is resumed when quitting
    }
}
