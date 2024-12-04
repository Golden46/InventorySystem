# InventorySlot / SetItem

### Declaration
public void SetItem(InventoryItem item)

### Returns
`None`

### Description
Adds the necessary `item data` onto the `slot`.

The below example is called when the player drags one `slot` onto another to swap the items. It sets the `data` on the first slot to the second and vice versa.
```cs
public void SwapItems(InventorySlot fromSlot, InventorySlot toSlot)
{
    InventoryItem fromItem = fromSlot.currentItem;
    fromSlot.SetItem(toSlot.currentItem);
    toSlot.SetItem(fromItem);
}
```
