# PlayerInventory
> This is the script which holds the functionality for the players inventory.

### Public Properties
|name|description|
|----|-----------|
|inventoryUI|Reference to the Inventory UI component|

### Private Properties
|name|description|
|----|-----------|
|_pInventory|Instance of the inventory script|
|_fpc|Reference to the First Person Controller Script|

### Public Methods
|name|description|
|-|-|
|[ToggleInventory](ToggleInventory.md)|Opens and closes the player inventory.|
|[PickupItem](PickupItem.md)|Attempts to add the `item` to the `Item` list and then updates the `UI` to display it.|
|[DropItem](DropItem.md)|Removes an `item` from the `Items` list if it exists and then updates the `UI`.|
|[SwapItems](SwapItems.md)|Calls the `SwapItems` function on the `Inventory` script.|

### Private Methods
|name|description|
|-|-|
|Start|Creates a new instance of an inventory and finds the FPS script.|
|[SetCursorState](SetCursorState.md)|Toggles the `visibility` and `lockState` of the cursor based on the open state of the `player inventory`.|
