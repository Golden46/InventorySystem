using System.Collections.Generic;
    
public class Inventory
{
    public List<InventoryItem> Items = new List<InventoryItem>();
    public int MaxSlots = 24; 

    public bool AddItem(InventoryItem item)
    {
        if (Items.Count >= MaxSlots) return false;
        Items.Add(item);
        return true;
    }

    public void SwapItems(InventoryItem fromItem, InventoryItem toItem)
    {
        if (Items.Contains(fromItem) && Items.Contains(toItem))
        {
            var idx = Items.IndexOf(fromItem);
            Items[Items.IndexOf(toItem)] = fromItem;
            Items[idx] = toItem;
        }
    }

    public void RemoveItem(InventoryItem item)
    {
        if (Items.Contains(item)) 
        {
            Items.Remove((item));
        }
    }
}