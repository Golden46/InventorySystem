# RemoveItem

### Declaration
public void RemoveItem(InventoryItem item)

### Returns
```None```

### Description
Removes an item from the Item list.

The below example creates a new ```Inventory``` ```instance``` for the player and has a function to remove an item from it.
```cs
private Inventory _playerInventory;

private void Start()
{
  _playerInventory = new Inventory(); 
}

private void DropItem(InventoryItem item)
{
  _playerinventory.RemoveItem(item)
}
```
