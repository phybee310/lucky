using UnityEngine;
using System.Collections.Generic;

public class TrashCan : MonoBehaviour
{
    public enum TrashCanType { Red, Yellow, Green, Blue }
    public TrashCanType trashCanType; // Set in Inspector

    public static int globalScore = 0; // Total score from all trash cans
    


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

    private readonly Dictionary<string, int> penaltyScores = new Dictionary<string, int>
    {
        { "chemical", -15 }, { "plaster", -15 }, { "battery", -15 },
        { "tin", -1 }, { "glass", -1 }, { "newspaper", -1 }, { "bottle", -1 },
        { "eggshell", -5 }, { "fishbone", -5 }, { "vegetable", -5 },
        { "carpet", -10 }, { "chair", -10 }, { "plastic bag", -10 }
    };

    public void StoreItem(Item item)
    {
        if (item == null) return;

        string itemName = item.itemName;

        if (validItems[trashCanType].Contains(itemName)) // Correct item for this trash can
        {
            globalScore += itemScores[itemName];
            Debug.Log($"{itemName} correctly placed in {trashCanType} trash can. Total global score: {globalScore}");
        }
        else // Incorrect item for this trash can
        {
            globalScore += penaltyScores[itemName];
            Debug.Log($"{itemName} incorrectly placed in {trashCanType} trash can. Total global score: {globalScore}");
        }
    }
    public static void ResetScore()
    {
        globalScore = 0;  // Reset the global score to zero
        Debug.Log("Global score reset to 0");
    }

}
