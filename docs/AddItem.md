# Inventory / AddItem

### Declaration
public bool AddItem(InventoryItem item)

### Returns
Boolean ```false``` if inventory is full and ```true``` if the item gets added.

### Description
Adds an item to the Item list.

The below example creates a new ```Inventory``` ```instance``` for the player and has a function to add an item to it. 
```cs
private Inventory _playerInventory;

private void Start()
{
  _playerInventory = new Inventory();
}

private void PickupItem(InventoryItem item)
{
  if(_pInventory.AddItem(item)) Debug.Log("Item added to inventory")
  else Debug.Log("Inventory Full")
}
```
