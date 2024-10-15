using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Inventory PInventory;
    public InventoryUI inventoryUI;

    private void Start()
    {
        PInventory = new Inventory(); // Initialises the player inventory
    }

    public bool PickupItem(InventoryItem item)
    {
        bool added = PInventory.AddItem(item);
        inventoryUI.UpdateInventory(PInventory);
        return added;
    }

    public void DropItem(InventoryItem item)
    {
        PInventory.RemoveItem(item); // Removes item from inventory
        inventoryUI.UpdateInventory(PInventory);
        // Drop item in world v
    }
}
