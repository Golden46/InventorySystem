using System.Collections.Generic;
    
public class Inventory
{
    public InventoryUI InventoryUI;
    public List<InventoryItem> Items = new List<InventoryItem>(); // List of items on the inventory
    public int MaxSlots = 64; // Maximum amount of slots on the inventory

    public bool AddItem(InventoryItem item)
    {
        if (Items.Count >= MaxSlots) return false;
        Items.Add(item);
        return true;
    }

    public void RemoveItem(InventoryItem item)
    {
        if (Items.Contains(item)) 
        {
            Items.Remove((item));
        }
    }
}