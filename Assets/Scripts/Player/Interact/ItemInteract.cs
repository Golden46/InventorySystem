using UnityEngine;

public class ItemInteract : Interactable
{
    public InventoryItem item;
    
    public override void OnFocus()
    {
    }

    public override void OnLoseFocus()
    {
    }

    public override void OnInteract()
    {
        PickUp();
    }

    private void PickUp()
    {
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();

        if (playerInventory == null) return;

        if (playerInventory.PickupItem(item))
        {
            Debug.Log($"Picked up {item.itemName}");
            Destroy(gameObject);  // Remove the item from the world
        }
        else Debug.Log("Inventory is full!");
    }
}
