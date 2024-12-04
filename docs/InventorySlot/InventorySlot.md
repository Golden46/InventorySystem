# InventorySlot
> This is the script which holds the functionality each individual slot in the inventory.

### Public Properties
|name|description|
|----|-----------|
|currentItem|The `Item` that is currently in the `slot`|
|icon|The `Item` `Image` being displayed in the `slot`|
|itemStats|The `ItemStats` being displayed on the `slot`|

### Private Properties
|name|description|
|-|-|
|statPanel|`Panel` which shows the `stats` of an `item` in a `slot` when hovered over.|
|itemName|Name of the `item` in the `slot`.|
|stats|Stats of the `item` in the `slot`|
|_playerInventory|Reference to the `PlayerInventory` component.|
|_playerTransform|Reference to the players `transform`|
|_originalParent|Stores the parent of the `image` in a `slot` so it can be pulled out and put back in.|
|_canvasGroup|Reference to the `CanvasGroup` on each `slots` `image`.|

### Public Methods
|name|description|
|-|-|
|[SetItem](SetItem.md)|Sets the `item data` in the slot.|
|[OnBeginDrag](OnBeginDrag.md)|Calls when player begins to drag a `slot` - Pulls out the `image` from the `slot`.|
|[OnDrag](OnDrag.md)|Calls when the player is dragging a `slot` - Makes the `image` follow the cursor.|
|[OnEndDrag](OnEndDrag.md)|Calls when the player stops dragging the `slot` - Puts the `image` back into the slot. If dropped out of the `inventory` it calls `DropItemInWorld()` and destroys the `slot`.|
|[OnDrop](OnDrop.md)|Calls when the player drops a `slot` onto another `slot` - Swaps the `Items` in the `slots`.|
|[OnPointerEnter](OnPointerEnter.md)|Opens the info panel for the `Item` in the `slot`.|
|[OnPointerExit](OnPointerExit.md)|Closes the info panel for the `Item` in the `slot`.|

### Private Methods
|name|description|
|-|-|
|Awake|Gets a reference to the `PlayerInventory`, `CanvasGroup` and players `transform`.|
|[SetItemStats](SetItemStats.md)|Sets the `ItemStats` in the slot.|
|[DropItemInWorld](DropItemInWorld.md)|Instantiates the `item` from the `inventory` into the world as a `GameObject` that can be picked up again.|
