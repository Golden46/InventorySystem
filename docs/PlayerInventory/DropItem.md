# PlayerInventory / Dropitem

### Declaration
public void Dropitem(InventoryItem item)

### Returns
`none`

### Description
Removes an `item` from the `Items` list if it exists and then updates the UI to reflect the change.

The `DropItemInWorld` function will call when the player drags a slot off of the screen. This script is on the slot. First it gets the position infront of the player to physically drop the item. It will then call the `DropItem` function on the `PlayerInventory` to remove it from the list. Lastly, it `instantiates` the physical `GameObject` of the `item` in the `PlayerInventory` in the position it gets at the start.
```cs
private PlayerInventory _playerInventory;
private Transform _playerTransform;

 private void Awake()
 {
     _playerInventory = FindObjectOfType<PlayerInventory>();
     _playerTransform = GameObject.FindWithTag("Player").transform;
 }

private void DropItemInWorld(InventoryItem currentItem)
{
    Vector3 dropPosition = _playerTransform.position + _playerTransform.right * 2;
    _playerInventory.DropItem(currentItem);
    Instantiate(currentItem.prefab, dropPosition, Quaternion.identity);
}
```
