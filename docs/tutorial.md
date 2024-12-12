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


## User Interface
> In this section we will be creating all of the visual and interactivity elements of the inventory.

### Script - [InventoryUI](InventoryUI/InventoryUI.md)
This script handles `updating` the `info` in the `slots` when items are `added` or `swapped` in the inventory. Create this script in `Scripts > Inventory > UI`

>[!NOTE]
> This script will not function at first. It requires the `InventorySlot` script which we will be making after. You should get a few errors until that script is created, you do not need to worry about them.

```cs
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform inventoryPanel;

    public List<InventorySlot> inventorySlots;

    public void UpdateInventory(Inventory playerInventory){...}

    public void SwapItems(InventorySlot fromSlot, InventorySlot toSlot){...}
}
```
<br>

The `UpdateInventory` Function fist loops through every slot in the inventory and deletes them all. Then for every item in the `Items` list it instantiates a new slot for that item under the `inventoryPanel` object. It then finds the `InventorySlot` script (which we will be creating next) on the slot and calls the `SetItem` function while passing in the item.
```cs
public void UpdateInventory(Inventory playerInventory)
{
    foreach (Transform child in inventoryPanel) Destroy(child.gameObject);
    
    foreach (var item in playerInventory.Items)
    {
        GameObject newSlot = Instantiate(slotPrefab, inventoryPanel);
        InventorySlot slotComponent = newSlot.GetComponent<InventorySlot>();
        slotComponent.SetItem(item);
    }
}
```
<br>

The `SwapItems` script is called in the `InventorySlot` script when an item is detected to be swapped in the inventory. It is very similar to the `SwapItems` function on the `Inventory` script but instead of swapping the position in a list, it is swapping the slot data around. 
```cs
public void SwapItems(InventorySlot fromSlot, InventorySlot toSlot)
{
    InventoryItem fromItem = fromSlot.currentItem;
    fromSlot.SetItem(toSlot.currentItem);
    toSlot.SetItem(fromItem);
}
```

>[!IMPORTANT]
> At this stage you need to remember to uncomment out the code in the `PlayerInventory` script from earlier if you did that to remove the errors. If you would like, you can do the same in this script for every reference to `InventorySlot` to prevent errors from popping up.

### Script - [InventorySlot](InventorySlot/InventorySlot.md)
This script handles the way slots are dragged, added and removed. Create this script in `Scripts > Inventory > UI`

The `Awake` function gets a reference to the `PlayerInventory` on the player, `CanvasGroup` on the icon of the slot, and `transform` of the player.

>[!NOTE]
> All of the "Handlers" are `interfaces` in `UnityEngine.EventSystems`. A simpler way to implement these "events" would be to use the `Event Trigger` componenent which only requires simple drag-and-drop functions with more limited coding. However making use of `interfaces` means it is: Fully customisable to your liking in code, runs faster therby being more efficient and direct, and `PointerEventData` is directly provided in the `Method` without having to cast `BaseEventData`. If you are confident enough, you would be fully able to slightly alter my code here to fit in with the use of `Event Triggers`.
```cs
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryItem currentItem;
    public Image icon;
    public string itemStats;

    [SerializeField] private GameObject statPanel;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI stats;

    private PlayerInventory _playerInventory;
    private Transform _playerTransform;
    private Transform _originalParent;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _playerInventory = FindObjectOfType<PlayerInventory>();
        _canvasGroup = icon.GetComponent<CanvasGroup>();
        _playerTransform = GameObject.FindWithTag("Player").transform;
    }

    public void SetItem(InventoryItem item){ ... }

    private void SetItemStats(InventoryItem item){ ... }

    public void OnBeginDrag(PointerEventData eventData){ ... }

    public void OnDrag(PointerEventData eventData){ ... }

    public void OnEndDrag(PointerEventData eventData){ ... }

    public void OnDrop(PointerEventData eventData){ ... }

    private void DropItemInWorld(){ ... }

    public void OnPointerEnter(PointerEventData eventData){ ... }

    public void OnPointerExit(PointerEventData eventData){ ... }
}
```
<br>

The `SetItem` function is called when an `item` is added to the `slot`. This could be when a new item is added or when items are swapped. First, the `InventoryItem` is set and then the `sprite` is changed to reflect the icon. Next, the name of the item is put in the `TextMeshProGUI` component to be displayed on the slot and the `SetItemStats` function is called.
```cs
public void SetItem(InventoryItem item)
{
    currentItem = item;
    icon.sprite = currentItem.itemIcon;

    itemName.text = currentItem.name;
    SetItemStats(currentItem);
}
```
<br>

The `SetItemStats` function resets the `itemStats` string and then pulls all of the stat names and values from the `item` `dictionary` and formats it into a string to be displayed to the player in the `inventory`.
>[!NOTE]
> You don't need a separate function for this, I just like to make my code look more readable and less crowded. If you like, you can move the contents of this function into the `SetItem` function.
```cs
private void SetItemStats(InventoryItem item)
{
    itemStats = "";
    foreach (KeyValuePair<string, int> kvp in item.ItemStats()) itemStats += $"{kvp.Key}: {kvp.Value}\n";
    stats.text = itemStats;
}
```
<br>

The `OnBeginDrag` event is called when the mouse is being held and has been dragged a certain `minimum threshold distance`. When this happens the `OnPointerExit` function is called which is explained further down. The next two lines saves the `slot` object in the `_originalParent` variable because the `icon` (which is a `child` of the `slot` object) gets taken out of the slot; This is so it can be dragged and easily swapped into a new slot. The last line of code makes sure to disable raycast blocking so other slots can be detected. 
```cs
public void OnBeginDrag(PointerEventData eventData)
{
    OnPointerExit(eventData);

    _originalParent = icon.transform.parent;
    icon.transform.SetParent(transform.root);
    _canvasGroup.blocksRaycasts = false;
}
```
<br>

The `OnDrag` event is called every frame when the object is being dragged. It makes the `icon` follow the `cursor` while it is being dragged.
```cs
public void OnDrag(PointerEventData eventData)
{
    icon.transform.position = eventData.position;
}
```
<br>

The `OnEndDrag` event is called when the dragging ends. At this point it does the opposite of `OnBeginDrag` and puts the `icon` back in the `slot` as a child. It then enables blocking raycasts on the Canvas group agian. If the item is dragged outside of the `inventory` then it calls the `DropItemInWorld` function, which is explained later, and then `destroys` the `slot`.
```cs
public void OnEndDrag(PointerEventData eventData)
{
    icon.transform.SetParent(_originalParent);
    icon.transform.localPosition = Vector3.zero;
    _canvasGroup.blocksRaycasts = true;

    if (!RectTransformUtility.RectangleContainsScreenPoint((RectTransform)transform.parent, eventData.position))
    {
        DropItemInWorld();
        Destroy(gameObject);
    }
}
```
<br>

The `OnDrop` event is called when an object accepts a drop. First it gets the `slot` that is being dragged into it and if it exists and isn't the same as itself it will swap the items.
```cs
public void OnDrop(PointerEventData eventData)
{
    InventorySlot draggedSlot = eventData.pointerDrag.GetComponent<InventorySlot>();

    if (draggedSlot != null && draggedSlot != this)
    {
        InventoryUI inventoryUI = GetComponentInParent<InventoryUI>();
        inventoryUI.SwapItems(draggedSlot, this);
        _playerInventory.SwapItems(draggedSlot.currentItem, this.currentItem);
    }
}
```
<br>

The `DropItemInWorld` function is called when an `item` is ejected from the inventory. It will `instatiate` the `GameObject` of the item infront of the player and also remove the item from the inventory.
```cs
private void DropItemInWorld()
{
    Vector3 dropPosition = _playerTransform.position + _playerTransform.right * 2;
    _playerInventory.DropItem(currentItem);
    Instantiate(currentItem.prefab, dropPosition, Quaternion.identity);
}
```
<br>

The `OnPointerEnter` function is used to show the displayed stats. There is no z-index for UI componenets so you have to pop the `stat panel` out of the `slot` and move it to the bottom after activating the panel in order to show it above the items in the inventory. `SetParent` changes moves that stat panel out a layer and `SetAsLastSibling` moves it to the end of the transform list.
```cs
public void OnPointerEnter(PointerEventData eventData)
{
    statPanel.SetActive(true);
    statPanel.transform.SetParent(transform.parent);
    statPanel.transform.SetAsLastSibling();
}
```
<br>

The `OnPointerExit` function is used to hide the displayed stats. It just hides the `GameObject` and makes it a child of the `slot` again.
```cs
public void OnPointerExit(PointerEventData eventData)
{
    statPanel.SetActive(false);
    statPanel.transform.SetParent(transform);
}
```

 ***

### Creating the Inventory UI.
First, create a new `Canvas` and call it `InventoryCanvas` then add the `InventoryUI` script to it. Now create a `Panel`, call it `ItemHolder`, and size it to how you want your inventory to look. Add the `Grid Layout Group` component to the `ItemHolder` and set the `Cell Size` to however big you want your slots to be - mine is `100x100`. You should also edit the `Spacing` and `Padding` until you get something you are happy with. To test how it looks create a `Button` under the `ItemHolder` and remove the `TextMeshProUGUI` component. Also, create a new `TextMeshProUGUI` component and set the text to `Inventory` and position it at the top of the `Panel`; Make this a child of `InventoryCanvas` not `ItemHolder`. 

![image](https://github.com/user-attachments/assets/22f1520e-e761-4cd5-8c34-f4bcec4d0eeb)

Now with that `Button` you created, name it to `Slot` and add the `InventorySlot` script to it. You also want to create an `Image` as a child and call it `Icon` - additionally, add the `Canvas Group` component to it. Then drag the `Icon GameObject` into the `Icon` slot on the `InventorySlot` script in the inspector. 

TALK ABOUT STAT PANEL NEXT
