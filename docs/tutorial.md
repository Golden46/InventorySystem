# Inventory System Tutorial
> This tutorial will cover how to create this inventory system for yourself and it will fully explain each section in detail.

## Prerequisites
Before being able to follow this tutorial you will need to have the correct version of Unity installed and a development IDE. 
This project was created using Unity 2022.3.46f1 and Rider, although you can use any code editor you like.

If you need help installing this version of unity please go [here](INSTALLUNITY.md) and follow the steps as needed.

Next, you will need to have a functioning first person controller with a way to interact with objects. You can use your own you made or from a tutorial, follow [my tutorial]() to create one, or [download mine](). This tutorial will be using Unitys New Input System however, if you are confident enough, the code can be easily edited to use the old system. 

Additionally, you will need basic understanding of a few intermediate programming concepts such as:
- Dictionaries
- Inheritance
- Attributes
- Scriptable Objects
- Abstract Classes
- Interfaces
- and More


## Information & Objectives
This tutorial is quite long, if you somehow miss where you were at, you can press the 3 lines in the top right to open the Outline to jump to specific points.


## Setting up the scene
First, you will need to create a Unity 3D project. This was created using the Universal 3D template with the Universal Render pipeline but it can be created the same using the Built-In Render Pipeline too. If you have downloaded my `First Person Controller` then you should already have a player you can move around. If not you will need your own.

Set up your project hierarchy as follows.
```
- Assets
    - Prefabs
        - Inventory
        - Ore
        - Weapons
    - Scripts
        - Inventory
            - Inventories
            - Items
                - Ore
                - Swords
            - UI
        - Player
            - Interact
    - Sprites
```
You can ignore the `Player` folder if you have your own controller; If you downloaded mine or followed my tutorial then it will already be there.


## Creating the Items
> In this section, we are going to be creating a way for us to store our `item data` which can then be used in the `inventory`. 

### Script - [Inventory Item](Items/InventoryItem.md)
To start off, we will create the `base class` for every item in our game. This class is going to contain all of the important `item information` needed for a functional inventory. Create this script in `Scripts > Inventory > Items`

> [!IMPORTANT]
> This `class` is `abstract` and is also a `Scriptable Object`; You should have previous knowledge of what both of these concepts are before attempting this tutorial.

- The `id` property is to be able to uniquely identify each item.
- The `itemName` property will hold the name of the `item`.
- The `itemIcon` property will store the `Sprite` of the item which is what will be displayed to the player in the `inventory`.
- In this tutorial we will not be making items stackable using the `isStackable` and `maxStackSize` properties, however you may add that on if you wish.
- Finally, the `prefab` property will store the `GameObject` attached to the item so when it gets dragged out of the `inventory` it can be `instantiated`
<br>

> [!NOTE]
> Simply, the class is made `abstract` because later on we are going to be storing every item currently in the `inventory` in a list of type `InventoryItem`. However, this class will never be used to create a `Scriptable Object`; All of our `item types` will have seperate scripts that all `inherit` from this `class`. This means that we are unable to access the `unique data` from each individual `item class`. We want to be able to access that data so the user can see the `stats` of each item. Making the class `abstract` allows us to create an `abstract` function at the bottom which will force it onto every class `inheriting` this one. So now we can call the `ItemStats` function and make a different one in each `item class` to return us the properties of that `item`.

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
> So you can easily view the data in the inspector, you should group your properties with `headers`. In this script I have added a header for the `item info` and `stack info`.

***

### Script - [Sword](Items/Sword.md)
Now that we have our base class, we can start to make some `item classes` we want in our game. Here is an example of an item class for a `sword`. It has a `sharpness`, `attack speed`, `guard ability`, and `durability` stat. Create this script in `Scripts > Inventory > Items > Swords`

>[!IMPORTANT]
> This item is a `Scriptable Object` and `inherits` from `InventoryItem`. This class will be used to create items. It is important to note that when an `item` is created using this class it will have both the properties from this class and the class it inherits from.

>[!NOTE]
> At the bottom we have the `ItemStats` function and as you can see it uses the `override` keyword. This is important because we are going to be `overidding` this for every `item class` we make so the correct stats get returned. Using this method to return the stats in the form of a `dictionary` works really well when you need to proceduraly show different types of stats.

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

### Script - [Ore](Items/Ore.md)
This is an example of another `item type` we could have in our game. You can make any items you want, you don't have to follow these exactly. Create this script in `Scripts > Inventory > Ore`

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

***

### Script - [ItemInteract](Items/ItemInteract.md)
This is the script that will be used in order to `interact` with the item in the world to place it in the `inventory`.

> [!WARNING]
> This script only works in conjunction with my first person controller. If you have downloaded my first person controller or followed my fps tutorial then it is fine yo use. If you have your own way to interact with items then you will need to adapt that to call the `PikcupItem` function every time the item is interacted with.

The `OnInteract` function is called when a player clicks on the item in the world. This then calls the `PickUp` function which gets the player inventory script and attempts to place it in the inventory. If the item goes into the inventory then the object gets destroyed. 

> [!NOTE]
> The `OnFocus` and `OnLoseFocus` functions can be ignored because we don't need to do anything in those for this interaction but they have to be there otherwise the script will not work. This is because they are present in the `Interactable` function this script overrides; The reason I haven't deleted them is because they could be useful for other interactions in the game. They are called when you hover over or look away from the item.
```cs
public class ItemInteract : Interactable
{
    public InventoryItem item;
    
    public override void OnFocus(){}

    public override void OnLoseFocus(){}

    public override void OnInteract(){ PickUp(); }

    private void PickUp()
    {
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();

        if (playerInventory == null) return;

        if (playerInventory.PickupItem(item))
        {
            Destroy(gameObject);
        }
    }
}
```

### Unity Editor - Creating the objects.
Now these are created, go into the `Assets > Inventory > Items` folder. Now if you `Right click > Create > InventoryItem > Sword` you should be able to create a `Sword` `item`.

![image](https://github.com/user-attachments/assets/2815306a-e15e-49e9-8db0-084f3ff403d3)

Above is an example of a sword I created with all the values edited. Now you can create as many swords as you want and this process can also be repeated for the ores.

Now you can create a prefab for the object in the world. First spawn in a `3D Cube` and attach the `ItemInteract` script to it or your equivalent. Whatever item you want that object to be, put the `ScriptableObject` item in the `Item` variable in the inspector. Then add a `RigidBody` so the item has physics. 

![image](https://github.com/user-attachments/assets/98572c43-45c9-4895-a84d-0c47281730dc)

Now you should be able to walk up to this object, and interact with it. Although at the moment it won't destroy because the `player inventory` script does not exist. You can add a `Debug.Log` in the `OnInteract` function and comment out the `Pickup` function to check if your interaction is working.

## Inventory Functionality
> In the section we will be creating the functional part of the system. We will be writing the code to `add`, `remove`, `swap` items and more. 

### Script - [Inventory](Inventory.md) 
This is the `base inventory class` for our system. All of our inventories we create will use this class as its foundation. Create this script in `Scripts > Inventory`

> [!IMPORTANT]
> You should realise that this class `does not` inherit from any scripting API such as `MonoBehaviour`. This is because the script will never be in the scene and does not require anything from `MonoBehaviour` to function as it is only used for backend work. Set the `MaxSlots` value to the maximum amount of slots your inventory will have and if this changes make sure to change this number to that value otherwise it will overflow.
 
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
<br>

The `AddItem` function first checks if the amount of items in the list is more than or equal to the `MaxSlots` in the Inventory. If it is then it will `return` `false` and not add the item to the Item list. If it isn't then the item will be added to the list.
```cs
public bool AddItem(InventoryItem item)
{
    if (Items.Count >= MaxSlots) return false;
    Items.Add(item);
    return true;
}
```
<br>

The `SwapItems` function first checks if the items being swapped are both in the `Items` list. If they are, it will get the index of the `fromItem` and put it into the placeholder variable `idx` for use later. It then gets the `index` of the `toItem` and sets the `fromItem` to that position. This is why we needed to store the `fromItem` index in that placeholder variable. The `fromItem` index is now the same as what the `toItem` index was at the start. We need the old position which is stored in the `idx` variable. So, lastly it will place the `toItem` into the position where the `fromItem` was originaly using the placeholder variable.

>[!WARNING]
> You need to have that placeholder variable. If you move the `fromItem` before storing its `index` in a placeholder variable then when you go to slot the `toItem` into its index it will have changed and you will be slotting the `toItem` back into its original position.
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
<br>

The `RemoveItem` function first checks if the item being removed exists in the list and if it does it simply removes it.
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

### Script - [PlayerInventory](PlayerInventory/PlayerInventory.md)
This is the script for the players inventory. Create this script in `Scripts > Inventory > Inventories`. When the script is done, it should be attached to the `Player` in the scene.

>[!IMPORTANT]
> This script inherits from `MonoBehaviour` and not `Inventory`. This is because we are using the `Inventory` script to create instances of it instead. Also, we will be creating the `InventoryUI` script after this script. However, this script requires that script to function. If you get an error due to missing the `InventoryUI` reference you can just comment out the code until we create the script. 

The `Start` function creates a new instance of an `Inventory` and then gets a reference to the `FirstPersonController` script. 

>[!NOTE]
> Make sure you change any reference to `FirstPersonController` to whatever your own FPS script is called unless you are using the one I have provided or the one I created in the tutorial which are both the same.
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

    public void ToggleInventory(InputAction.CallbackContext context){ ... }

    private void SetCursorState(bool isInventoryOpen){ ... }

    public bool PickupItem(InventoryItem item){ ... }

    public void DropItem(InventoryItem item){ ... }

    public void SwapItems(InventoryItem fromItem, InventoryItem toItem){ ... }
}
```
<br>

The `ToggleInventory` function is used to open and close the player inventory. First it checks the state of the inventory; If it is open the `isInventoryOpen` bool will be `false` and vice versa. It then sets toggles the `InventoryUI` `GameObject` on or off. Next, the `cursor state` will be toggled using the next function. Lastly, the player will be unable to look around and interact when the inventory is open to prevent any unwanted functionality like accidently picking up an item while trying to drag an item in the inventory.
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
<br>

The `SetCursorState` function toggles the `visiblity` and `lockState` of the cursor on the screen. If the inventory is open the cursor becomes `visible` and `unlocked` and if the inventory is closed it becomes `invisible` and `locked`.
```cs
private void SetCursorState(bool isInventoryOpen)
{
    Cursor.visible = isInventoryOpen;
    Cursor.lockState = isInventoryOpen ? CursorLockMode.None : CursorLockMode.Locked;
}
```
<br>

The `PickupItem` function first attempts to add the `item` to the `Item` list and then updates the `UI` to display it. It then `returns` `true` or `false` depending on whether the item was added or not.
```cs
public bool PickupItem(InventoryItem item)
{
    bool added = _pInventory.AddItem(item);
    inventoryUI.UpdateInventory(_pInventory);
    return added;
}
```
<br>

The `DropItem` function removes the `item` from the `Item` list if it exists and then updates the `UI` to represent that.
```cs
public void DropItem(InventoryItem item)
{
    _pInventory.RemoveItem(item); 
    inventoryUI.UpdateInventory(_pInventory);
}
```
<br>

The `SwapItems` function just swaps the `items` in the `Item` list. The visual `UI` changes are done in the `InventorySlot` script where this function will get called from not the `InventoryUI` script.
```cs
public void SwapItems(InventoryItem fromItem, InventoryItem toItem)
{
    _pInventory.SwapItems(fromItem, toItem);
}
```
