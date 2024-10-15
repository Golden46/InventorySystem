using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "InventoryItem")]
public class InventoryItem : ScriptableObject
{
    [Header("Item Info")]
    public int id; // Unique item ID
    public string itemName; // Name of the item
    public Sprite itemIcon; // Icon to display in the GUI

    [Header("Stack Information")]
    public bool isStackable; // Can the item stack or is it single?
    public int maxStackSize; // How many items can stack
}