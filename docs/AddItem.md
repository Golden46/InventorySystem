# AddItem

### Declaration
public bool AddItem(InventoryItem item)

### Returns
Boolean ```false``` if inventory is full and ```true``` if the item gets added.

### Description
Adds an item to the Item list.

The below example creates a new inventory instance for the player and adds an item to it. 
```cs
private Inventory _playerInventory;
public InventoryItem item;

private void Start()
{
  _playerInventory = new Inventory();
}

private void PickupItem()
{
  if(_pInventory.AddItem(item)) Debug.Log("Item added to inventory")
  else Debug.Log("Inventory Full")
}
```
