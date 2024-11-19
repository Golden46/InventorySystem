# SwapItems

### Declaration
public void SwapItems(InventoryItem fromItem, InventoryItem toItem)

### Returns
```None```

### Description
Swaps two items that already exist in the Item list

The below example is the unity event ```IDropHandler``` and is on the ```InventorySlot```. If a slot is dragged over another slot it will swap the ```Items``` in the list. 
```cs
public void OnDrop(PointerEventData eventData)
{
  InventorySlot draggedSlot = eventData.pointerDrag.GetComponent<InventorySlot>();

  if (draggedSlot != null && draggedSlot != this)
    {
      _playerInventory.SwapItems(draggedSlot.currentItem, this.currentItem);
    }
}
```
