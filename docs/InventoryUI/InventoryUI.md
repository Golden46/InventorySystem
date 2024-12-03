# InventoryUI
> This script handles the updating of the inventory and the swapping of items

### Public Properties
|name|description|
|----|-----------|
|slotPrefab|Slot prefab used to create new slots.|
|inventoryPanel|The `GameObject` component which is the `parent` for all of the slots.|
|inventorySlots|List of all the current `inventory slots` active in the scene.|

### Public Methods
|name|description|
|-|-|
|[UpdateInventory](UpdateInventory.md)|Updates the `slots` every time an `item` change happens.|
|[SwapItems](SwapItemsUI.md)|Switches the `item data` between `slots`.|
