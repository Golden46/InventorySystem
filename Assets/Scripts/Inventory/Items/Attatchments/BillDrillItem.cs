using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "InventoryItem/BillDrill")]
public class BillDrillItem : InventoryItem
{
    [Header("Unique Item Info")]
    [Range(1,5)] public int miningPower; // Determines what it can mine
    public int maxDurability; // Determines how much you can mine before it breaks
}
