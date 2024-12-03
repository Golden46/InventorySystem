# PlayerInventory / PickupItem

### Declaration
public bool PickupItem(InventoryItem item)

### Returns
Boolean `false` if inventory is full and `true` if the item gets added.

### Description
Attempts to add the `item` to the `Item` list and updates the `UI` to reflect the change.

The below example will call the `PickUp` function when the player interacts with an item. This script is on the item. It will then call the `PickupItem` script with the item the player has clicked on. if the return is `true` then the item will get destroyed in the world because it has been added to the inventory. If not, the inventory is full so the item will not be destroyed in the world.
```cs
public InventoryItem item;

public override void OnInteract(){ PickUp(); }

private void PickUp()
{
    PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();

    if (playerInventory == null) return;

    if (playerInventory.PickupItem(item))
    {
        Debug.Log($"Picked up {item.itemName}");
        Destroy(gameObject);
    }
    else Debug.Log("Inventory is full!");
}
```
