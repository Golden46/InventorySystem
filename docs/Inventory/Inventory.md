# Inventory
> Base inventory class for our system which all inventories will use for functionality

### Public Properties
|name|description|
|----|-----------|
|Items|List of items in the inventory in the order that they appear|
|MaxSlots|How many slots the inventory can hold|

### Methods
|name|description|
|-|-|
|[AddItem](AddItem.md)|Adds item to the Items list if there is a free slot.|
|[SwapItems](../PlayerInventory/SwapItems.md)|Swaps two items in the Items list that have been dragged by the player.|
|[RemoveItem](RemoveItem.md)|Removes an item from the Items list if it exists.|
