using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Sword Item", menuName = "InventoryItem/Sword")]
public class Sword : InventoryItem
{
    [Header("Unique Item Info")]
    [Range(1,5)] public int sharpness;
    [Range(1,10)] public int attackSpeed;
    [Range(1,5)] public int guardAbility;
    [Range(1,500)] public int durability;

    public override Dictionary<string, int> ItemStats()
    {
        return new Dictionary<string, int>
        {
            { "Sharpness", sharpness },
            { "Attack Speed", attackSpeed },
            { "Guard Ability", guardAbility },
            { "Durability", durability }
        };
    }
}