# Player Inventory Tutorial - UI

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

