using UnityEngine;

public class InventoryItem : ScriptableObject
{
    [Header("Item Info")]
    public int id; 
    public string itemName; 
    public Sprite itemIcon;

    [Header("Stack Information")]
    public bool isStackable; 
    public int maxStackSize;
}