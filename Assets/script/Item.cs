using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;    // Name of the item
    public int scoreValue;     // Score value of the item
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

}
