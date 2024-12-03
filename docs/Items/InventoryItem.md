# InventoryItem
> Base abstract class for every item. Includes all the data every item will collectively share.

### Public Properties
|name|description|
|----|-----------|
|id|Unique identifier for the item|
|itemName|Name of the item|
|itemIcon|Sprite that displays on the inventory|
|isStackable|Determines whether the item can stack|
|maxStackSize|Maximum number of items in one stack|
|prefab|The object in world|

### Abstract methods
|name|description|
|-|-|
|[ItemStats](ItemStats.md)|Gets the stats of an item|

