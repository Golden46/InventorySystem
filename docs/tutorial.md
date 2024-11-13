# Inventory System Tutorial
> This tutorial will cover how to create this inventory system for yourself and it will fully explain each section in detail.


## Quick Links
- [Prerequisites](#prerequisites)
- [Objectives](#objectives)
- [Setting up the scene](#setting-up-the-scene)
- [Scripting - Items](#scripting---items)
  - [Inventory Item](#inventory-item)
  - [Sword](#sword)
  - [Ore](#ore)
    

## Prerequisites
Before being able to follow this tutorial you will need to have the correct version of Unity installed and a development IDE. 
This project was created using Unity 2022.3.46f1 and Rider, although you can use any code editor you like.

If you need help installing this version of unity please go [here](INSTALLUNITY.md) and follow the steps as needed.

Additionally, you will need basic understanding of a few intermediate programming concepts:
- Dictionaries
- Inheritance
- Attributes
- Scriptable Objects
- Abstract Classes


## Objectives


## Setting up the scene
First, you will need to create a Unity 3D project. This was created using the Universal 3D template with the Universal Rednder
pipeline but it can be created the same using the Built-In Render Pipeline too.


## Scripting - Items
In this section, we are going to be creating a way for us to store our item data which can then be used in the inventory. 

### [Inventory Item](InventoryItem.md)
To start off, we will create the base class for every item in our game. This class is going to contain all of the important item information needed
for a functional inventory.

> [!IMPORTANT]
> This class is abstract and a Scriptable Object; You should have previous knowledge of what these things are before attempting this tutorial.

> [!NOTE]
> This Scriptable Object will never be created as an asset we can use. All of our item types will have seperate objects that all inherit from this class.
> The abstract function at the bottom of the script will be overriden later on in our item classes order to get the item stats from each seperate item so
> it can be displayed in our inventory.

```cs
using UnityEngine;
using System.Collections.Generic;

public abstract class InventoryItem : ScriptableObject
{
    [Header("Item Info")]
    public int id; 
    public string itemName; 
    public Sprite itemIcon;

    [Header("Stack Info")]
    public bool isStackable; 
    public int maxStackSize;

    public GameObject prefab;

    public abstract Dictionary<string, int> ItemStats();
}
```

> [!TIP]
> So you can easily view the data in the inspector, you should add headers to your properties. In this script I have added a header for the item info and
> stack info to help organise it.


### [Sword](Sword.md)
Now that we have our base class, we can start to make some item classes we want in our game. Here is an example of an item class for
a sword. It has a sharpness, attack speed, guard ability, and durability stat.

>[!IMPORTANT]
> This item is also a Scriptable Object and will be created as an asset. It is important to realise that it inherits from InventoryItem because we want
> to have those properties on our sword as well as the ones that are unique to just this item.

>[!NOTE]
> The ItemStats function at the bottom is called the exact same as the one in the InventoryItem script. This is really important
> because it needs to override the function in the base class in order to work properly. We need a way to get the items stats and
> display them on the screen, and using this method to return them in the form of a dictionary works really well when you need to
> proceduraly show different types of stats.

```cs
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
```

>[!TIP]
> To be able to easily create an item data object, I have used the CreateAssetMenu attribute on top of the class. The fileName property will be the default name of the item when created. The menuName property will be where you go to create the item data in the context menu.

>[!TIP]
> Infront of each of the unique item properties, I have added a Range attribute. This is so I can limit, in the inspector, what values each property can go up to. This is good for being able to limit the values on each property so you dont accidently make an error when setting up an item.


### [Ore](Ore.md)
This is an example of another item type we could have in our game. You can make any items you want, you don't have to follow these exactly.

>[!NOTE]
> The layout and functionality is exactly the same; The only things that changes is every instance of the name of the item and the properites associated with that item.

```cs
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
```
