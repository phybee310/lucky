using UnityEngine;
using TMPro;

public class BonusItem : MonoBehaviour
{
    public int bonusPoints = 30; // Points awarded for picking up this item

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    public void ResetState()
    {
        transform.position = initialPosition;
        gameObject.SetActive(true);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Automatically collect the item when the player collides
            CollectItem();
        }
    }

    public void CollectItem()
    {
        // Add bonus points to the global score
        TrashCan.globalScore += bonusPoints;
        Debug.Log($"Bonus item collected! +{bonusPoints} points. Total Score: {TrashCan.globalScore}");
       

        // Destroy the bonus item
        Destroy(gameObject);
    }

    
}
