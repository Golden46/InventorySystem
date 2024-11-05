using UnityEngine;
using System.Collections.Generic;

public abstract class InventoryItem : ScriptableObject
{
    [Header("Item Info")]
    public int id; 
    public string itemName; 
    public Sprite itemIcon;

    [Header("Stack Information")]
    public bool isStackable; 
    public int maxStackSize;

    public GameObject prefab;

    public abstract Dictionary<string, int> ItemStats();
}