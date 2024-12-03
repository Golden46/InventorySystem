# InventoryUI / UpdateInventory

### Declaration
public void UpdateInventory(Inventory playerInventory)

### Returns
`None`

### Description
Updates the `slots` every time an `item` change happens such as picking up and dropping `items`.

The below example will call the `DropItem` function when an `item` is dragged out of the `inventory`. 
```cs
private Inventory _pInventory;
public InventoryUI inventoryUI;

private void Start(){ _pInventory = new Inventory(); }

public void DropItem(InventoryItem item)
{
    _pInventory.RemoveItem(item); 
    inventoryUI.UpdateInventory(_pInventory);
}
```
