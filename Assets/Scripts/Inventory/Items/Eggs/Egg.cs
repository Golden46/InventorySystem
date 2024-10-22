using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "InventoryItem/Egg")]
public class Egg : InventoryItem
{
    [Header("Unique Item Info")] 
    public string chickenType;
    public float hatchRate;
}

