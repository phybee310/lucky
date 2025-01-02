using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel; // Reference to the Game Over panel
    public static int globalScore = 0;
    

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
        // Reset the global score
        TrashCan.ResetScore();

        // Reset item states
        ResetAllItems();

        // Reload the current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

        // Resume the game
        Time.timeScale = 1;
    }

    private void ResetAllItems()
    {
        // Find all objects tagged as "Item" and "BonusItem" in the scene
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        GameObject[] bonusItems = GameObject.FindGameObjectsWithTag("BonusItem");

        // Reset each item's state
        foreach (GameObject item in items)
        {
            Item itemComponent = item.GetComponent<Item>();
            if (itemComponent != null)
            {
                itemComponent.ResetState();
            }
        }

        foreach (GameObject bonusItem in bonusItems)
        {
            BonusItem bonusItemComponent = bonusItem.GetComponent<BonusItem>();
            if (bonusItemComponent != null)
            {
                bonusItemComponent.ResetState();
            }
        }
    }

// Return to the main menu
public void GoToHome()
    {
        SceneManager.LoadScene("MainMenu"); // Replace with your actual main menu scene name
        Time.timeScale = 1;
    }
}
