using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "InventoryItem/Ore")]
public class Ore : InventoryItem
{
    [Header("Unique Item Info")] 
    [Range(1, 3)] public int furnacePower;
    [Range(60,120)] public int smeltTime;
    
    public override Dictionary<string, int> ItemStats()
    {
        return new Dictionary<string, int>
        {
            { "Furnace Req", furnacePower },
            { "Smelt Time", smeltTime }
        };
    }
}

