using UnityEngine;

public class ItemInteract : Interactable
{
    public InventoryItem item;
    
    public override void OnFocus()
    {
        Debug.Log("Focus");
    }

    public override void OnLoseFocus()
    {
        Debug.Log("Lose Focus");
    }

    public override void OnInteract()
    {
        Debug.Log("Interact");
        PickUp();
    }

    private void PickUp()
    {
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();

        if (playerInventory == null) return;
        bool wasPickedUp = playerInventory.PickupItem(item);

        if (wasPickedUp)
        {
            Debug.Log($"Picked up {item.itemName}");
            Destroy(gameObject);  // Remove the item from the world
        }
        else Debug.Log("Inventory is full!");
    }
}
