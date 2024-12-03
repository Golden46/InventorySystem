# InventoryUI / SwapItems

### Declaration
public void SwapItems(InventorySlot fromSlot, InventorySlot toSlot)

### Returns
```None```

### Description
Swaps out `item data` from one slot to another.

The below example is the unity event ```IDropHandler``` and is on the ```InventorySlot```. If a slot is dragged over another slot it will swap the ```item data``` in the list. 
```cs
public void OnDrop(PointerEventData eventData)
{
  InventorySlot draggedSlot = eventData.pointerDrag.GetComponent<InventorySlot>();

  if (draggedSlot != null && draggedSlot != this)
  {
    InventoryUI inventoryUI = GetComponentInParent<InventoryUI>();
    inventoryUI.SwapItems(draggedSlot, this);
  }
}
```
