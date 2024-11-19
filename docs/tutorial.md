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
- [Scripting - Inventory Functionality](#scripting---inventory-functionality)
  - [Inventory](#inventory)
- [Scripting - User Interface](#scripting---user-interface)

## Prerequisites
Before being able to follow this tutorial you will need to have the correct version of Unity installed and a development IDE. 
This project was created using Unity 2022.3.46f1 and Rider, although you can use any code editor you like.

If you need help installing this version of unity please go [here](INSTALLUNITY.md) and follow the steps as needed.

Additionally, you will need basic understanding of a few intermediate programming concepts:
- Dictionaries
- Inheritance
- Attributes
- Unitys New Input System
- Scriptable Objects
- Abstract Classes


## Objectives


## Setting up the scene
First, you will need to create a Unity 3D project. This was created using the Universal 3D template with the Universal Rednder pipeline but it can be created the same using the Built-In Render Pipeline too.


## Scripting - Items
> In this section, we are going to be creating a way for us to store our item data which can then be used in the inventory. 

### [Inventory Item](InventoryItem.md)
To start off, we will create the base class for every item in our game. This class is going to contain all of the important item information needed
for a functional inventory.

> [!IMPORTANT]
> This class is abstract and a `Scriptable Object`; You should have previous knowledge of what these things are before attempting this tutorial.

> [!NOTE]
> This `Scriptable Object` will never be created as an asset we can use. All of our item types will have seperate objects that all inherit from this class. The abstract function at the bottom of the script will be overriden later on in our item classes order to get the item stats from each seperate item so it can be displayed in our inventory.

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
> So you can easily view the data in the inspector, you should add headers to your properties. In this script I have added a header for the item info and stack info to help organise it.

***

### [Sword](Sword.md)
Now that we have our base class, we can start to make some item classes we want in our game. Here is an example of an item class for a sword. It has a sharpness, attack speed, guard ability, and durability stat.

>[!IMPORTANT]
> This item is also a `Scriptable Object` and will be created as an asset. It is important to realise that it inherits from `InventoryItem` because we want to have those properties on our sword as well as the ones that are unique to just this item.

>[!NOTE]
> The `ItemStats` function at the bottom is called the exact same as the one in the `InventoryItem` script. This is really important because it needs to override the function in the base class in order to work properly. We need a way to get the items stats and display them on the screen, and using this method to return them in the form of a `dictionary` works really well when you need to proceduraly show different types of stats.

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
> To be able to easily create an item data object, I have used the `CreateAssetMenu` attribute on top of the class. The `fileName` property will be the default name of the item when created. The `menuName` property will be where you go to create the item data in the context menu.

>[!TIP]
> Infront of each of the unique item properties, I have added a `Range` attribute. This is so I can limit, in the inspector, what values each property can go up to. This is good for being able to limit the values on each property so you dont accidently make an error when setting up an item.

***

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


## Scripting - Inventory Functionality
> In the section we will be creating the functional part of the system. We will be writing the code to add, remove, swap items and more.

### [Inventory](Inventory.md)
This is the base inventory class for our system. All of our inventories we create will use this class as its foundation.

> [!IMPORTANT]
> You should realise that this class does not inherit from any scripting API such as `MonoBehaviour`. This is because the script will never be in the scene and does not require anything from `MonoBehaviour` to function as it is only used for backend work.
 
```cs
using System.Collections.Generic;
    
public class Inventory
{
    public List<InventoryItem> Items = new List<InventoryItem>();
    public int MaxSlots = 24; 

    public bool AddItem(InventoryItem item){...}

    public void SwapItems(InventoryItem fromItem, InventoryItem toItem){...}

    public void RemoveItem(InventoryItem item){...}
}
```
>[!NOTE]
> The `AddItem` function first checks if the amount of Items in the list is more than or equal to the MaxSlots in the Inventory. If it is then it will return false and not add the item to the Item list. If it isn't then the item will be added to the list.
```cs
public bool AddItem(InventoryItem item)
{
  if (Items.Count >= MaxSlots) return false;
  Items.Add(item);
  return true;
}
```
>[!NOTE]
> The `SwapItems` function first checks if the items being swapped are both in the Items list. If they are it will get the index of the first item and put it into a a placeholder variable for use later. It then gets the index of the second item and sets the first item to that index. Lastly it will place the second item into the index of the first item.

>[!WARNING]
> You need to have that placeholder variable. If you switch the first two items before getting both of the indexes then when you go to get the second items index it will have already changed and you will have no reference of where it came from.
```cs
public void SwapItems(InventoryItem fromItem, InventoryItem toItem)
{
  if (Items.Contains(fromItem) && Items.Contains(toItem))
  {
    var idx = Items.IndexOf(fromItem);
    Items[Items.IndexOf(toItem)] = fromItem;
    Items[idx] = toItem;
  }
}
```
>[!NOTE]
> The `RemoveItem` function first checks if the item being removed exists in the list and if it does it simply removes it.
```cs
public void RemoveItem(InventoryItem item)
{
  if (Items.Contains(item)) 
  {
    Items.Remove((item));
  }
}
```

***

### [PlayerInventory](PlayerInventory.md)
This is the script for the players inventory.

>[!IMPORTANT]
> This script inherits from `MonoBehaviour` and not `Inventory`. This is because we are using the `Inventory` script to create instances of it instead. Also, we will be creating the `InventoryUI` script after this script. However, this script requires that script to function. If you get an error due to missing the `InventoryUI` reference you can just comment out the code until we create the script. 
```cs
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    private Inventory _pInventory;
    public InventoryUI inventoryUI;

    private FirstPersonController _fpc;

    private void Start()
    {
        _pInventory = new Inventory(); 
        _fpc = FindObjectOfType<FirstPersonController>();
    }

    public void ToggleInventory(InputAction.CallbackContext context){...}

    private void SetCursorState(bool isInventoryOpen){...}

    public bool PickupItem(InventoryItem item){...}

    public void DropItem(InventoryItem item){...}

    public void SwapItems(InventoryItem fromItem, InventoryItem toItem){...}
}
```
>[!NOTE]
> The `ToggleInventory` function is used to open and close the player inventory. First it checks the state of the inventory; If it is open the `isInventoryOpen` bool will be false and vice versa. It then sets toggles the 'InventoryUI' 'GameObject' on or off. Next, the cursor state will be toggled using the next function. Lastly, the player will be unable to look around and interact when the inventory is open to prevent any unwanted functionality like accidently picking up an item while trying to drag an item in the inventory.
```cs
public void ToggleInventory(InputAction.CallbackContext context)
{
  bool isInventoryOpen = !inventoryUI.gameObject.activeInHierarchy;
  inventoryUI.gameObject.SetActive(isInventoryOpen);
  SetCursorState(isInventoryOpen);

  _fpc.canLook = !isInventoryOpen;
  _fpc.canInteract = !isInventoryOpen;
}
```
>[!NOTE]
> The `SetCursorState` function toggles the `visiblity` and `lockState` of the cursor on the screen. If the inventory is open the cursor becomes `visible` and `unlocked` and if the inventory is closed it becomes `invisible` and `locked`.
```cs
private void SetCursorState(bool isInventoryOpen)
{
  Cursor.visible = isInventoryOpen;
  Cursor.lockState = isInventoryOpen ? CursorLockMode.None : CursorLockMode.Locked;
}
```
>[!NOTE]
> The `PickupItem` function first attempts to add the `item` to the `Item` list and then updates the `UI` to display it. It then `returns true or false` depending on whether the item was added or not.
```cs
public bool PickupItem(InventoryItem item)
{
  bool added = _pInventory.AddItem(item);
  inventoryUI.UpdateInventory(_pInventory);
  return added;
}
```
>[!NOTE]
> The `DropItem` function removes the `item` from the `Item` list if it exists and then updates the `UI` to represent that.
```cs
public void DropItem(InventoryItem item)
{
  _pInventory.RemoveItem(item); 
  inventoryUI.UpdateInventory(_pInventory);
}
```
>[!NOTE]
> The `SwapItems` function just swaps the `items` in the `Item` list. The visual `UI` changes are done in the `InventorySlot` script where this function will get called from not the `InventoryUI` script.
```cs
public void SwapItems(InventoryItem fromItem, InventoryItem toItem)
{
  _pInventory.SwapItems(fromItem, toItem);
}
```

## Scripting - User Interface
