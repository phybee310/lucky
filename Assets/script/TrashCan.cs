using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class TrashCan : MonoBehaviour
{
    public enum TrashCanType { Red, Yellow, Green, Blue }
    public TrashCanType trashCanType;

    public static int globalScore = 0;

    public GameObject feedbackPanel;
    public TextMeshProUGUI feedbackText;

    private readonly Dictionary<TrashCanType, List<string>> validItems = new Dictionary<TrashCanType, List<string>>
    {
        { TrashCanType.Red, new List<string> { "chemical", "plaster", "battery" } },
        { TrashCanType.Yellow, new List<string> { "tin", "glass", "newspaper", "bottle" } },
        { TrashCanType.Green, new List<string> { "eggshell", "fishbone", "vegetable" } },
        { TrashCanType.Blue, new List<string> { "carpet", "chair", "plastic bag" } }
    };

    private readonly Dictionary<string, int> itemScores = new Dictionary<string, int>
    {
        { "chemical", 25 }, { "plaster", 25 }, { "battery", 25 },
        { "tin", 5 }, { "glass", 5 }, { "newspaper", 5 }, { "bottle", 5 },
        { "eggshell", 10 }, { "fishbone", 10 }, { "vegetable", 10 },
        { "carpet", 15 }, { "chair", 15 }, { "plastic bag", 15 }
    };

    private WinningPanelManager winningPanelManager;

    private void Start()
    {
        feedbackPanel?.SetActive(false);
        winningPanelManager = FindFirstObjectByType<WinningPanelManager>();
    }

    public void StoreItem(Item item)
    {
        if (item == null) return;

        string itemName = item.itemName;
        bool isCorrect = validItems[trashCanType].Contains(itemName);

        if (isCorrect)
        {
            globalScore += itemScores[itemName];
            ShowFeedback($"Correct! +{itemScores[itemName]} points.");
        }
        else
        {
            ShowFeedback("Incorrect item! No points awarded.");
        }

        winningPanelManager?.OnItemPlacedInTrashCan();
    }

    private void ShowFeedback(string message)
    {
        feedbackText.text = message;
        feedbackPanel.SetActive(true);
        Invoke(nameof(HideFeedback), 3f);
    }

    private void HideFeedback()
    {
        feedbackPanel?.SetActive(false);
    }

    public static void ResetScore()
    {
        globalScore = 0; // Reset the global score to zero
        Debug.Log("Global score reset to 0");
    }

}
