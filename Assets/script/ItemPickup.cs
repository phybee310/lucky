using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ItemPickup : MonoBehaviour
{
    public Button pickUpButton; // UI button to pick up items
    private GameObject itemToPickUp; // Currently interactable item
    private TrashCan trashCanToInteractWith; // Currently interactable trash can
    public List<GameObject> inventory = new List<GameObject>(); // Player's inventory
    public int maxInventorySize = 1; // Max number of items allowed in inventory

    void Start()
    {
        pickUpButton.interactable = false; // Button not interactable initially
        pickUpButton.onClick.AddListener(OnButtonClick); // Listen for button click
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            // Player is near an item to pick up
            itemToPickUp = other.gameObject;
            pickUpButton.interactable = true; // Enable button to pick up item
        }
        else if (other.CompareTag("TrashCan"))
        {
            // Player is near a trash can
            trashCanToInteractWith = other.GetComponent<TrashCan>();
            if (trashCanToInteractWith != null)
            {
                pickUpButton.interactable = true; // Enable button to interact with trash can
            }
        }
       
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            // Clear item reference when player leaves item range
            itemToPickUp = null;
            ResetPickUpButton(); // Disable button
        }
        else if (other.CompareTag("TrashCan"))
        {
            // Clear trash can reference when player leaves trash can range
            trashCanToInteractWith = null;
            ResetPickUpButton();
        }
        
    }

    void OnButtonClick()
    {
        if (itemToPickUp != null && inventory.Count < maxInventorySize)
        {
            // Player clicks button to pick up regular item
            PickUpItem();
        }
        else if (trashCanToInteractWith != null)
        {
            // Player clicks button to store item in trash can
            StoreItemInTrashCan();
        }
    }

    
    void ResetPickUpButton()
    {
        if (itemToPickUp != null || trashCanToInteractWith != null)
        {
            // Enable button when player is near an item or trashcan
            pickUpButton.interactable = true;
            Debug.Log("Button re-enabled for item or trash can interaction.");
        }
        else
        {
            // Disable button if no interactable object is in range
            pickUpButton.interactable = false;
            Debug.Log("Button disabled. No interactable object in range.");
        }
    }
    void PickUpItem()
    {
        // Regular item pickup logic
        if (itemToPickUp != null && inventory.Count < maxInventorySize)
        {
            Item itemComponent = itemToPickUp.GetComponent<Item>(); // Get item data
            if (itemComponent != null)
            {
                // Add item to inventory and log it
                inventory.Add(itemToPickUp);
                Debug.Log($"Picked up: {itemComponent.itemName} (Score: {itemComponent.scoreValue})");

                itemToPickUp.SetActive(false); // Deactivate the item
                pickUpButton.interactable = false; // Disable button

                ResetPickUpButton();
            }
        }
    }

    void StoreItemInTrashCan()
    {
        if (inventory.Count > 0)
        {
            // Assume the player has the first item in their inventory to store
            GameObject itemToStore = inventory[0];
            Item itemComponent = itemToStore.GetComponent<Item>(); // Get item data
            if (itemComponent != null)
            {
                // Store the item in the trash can and update score
                trashCanToInteractWith.StoreItem(itemComponent);

                // Remove the item from the inventory
                inventory.RemoveAt(0);

                // Deactivate item and reset the UI button
                itemToStore.SetActive(false);
                pickUpButton.interactable = false; // Disable button

                ResetPickUpButton() ;
            }
        }
    }
}
