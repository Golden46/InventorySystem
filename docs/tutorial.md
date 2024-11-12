# Inventory System Tutorial
> This tutorial will cover how to create this inventory system for yourself and it will fully explain each section in detail.

## Prerequisites
Before being able to follow this tutorial you will need to have the correct version of Unity installed and a development IDE. 
This project was created using Unity 2022.3.46f1 and Rider, although you can use any code editor you like.

If you need help installing this version of unity please go [here](INSTALLUNITY.md) and follow the steps as needed.

Additionally, you will need basic understanding of a few intermediate programming concepts:
- Scriptable Objects
- Dictionaries
- Abstract Classes
- Inheritance

## Objectives

## Quick Links
- [Items](#items)
  - [Inventory Item](#inventoryitem)
  - [Sword](#sword)
  -
-
-
-

## Setting up the scene
First, you will need to create a Unity 3D project. This was created using the Universal 3D template with the Universal Rednder
pipeline but it can be created the same using the Built-In Render Pipeline too.

## Items
### [Inventory Item](InventoryItem.md)
To start off, we will create the base class for every item in our game. This class is going to contain all of the important item information needed
for a functional inventory.

> [!IMPORTANT]
> This class is abstract and a Scriptable Object; You should have previous knowledge of what this is before attempting this tutorial.

> [!NOTE]
> This Scriptable Object will never be created as an asset we can use. All of our item types will have seperate objects that all inherit from this class.
> The abstract function at the bottom of the script will be used later on in order to get the item stats from each seperate item so it can be displayed
> in our inventory.

```cs
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
```

### [Sword](Sword.md)
Now that we have our base class, we can start to make some item classes we want in our game.

```cs
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Item", menuName = "InventoryItem/Sword")]
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
```
