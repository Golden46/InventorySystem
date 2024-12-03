# PlayerInventory / SetCursorState

### Declaration
private void SetCursorState(bool isInventoryOpen)

### Returns
`None`

### Description
Toggles the `lockState` and `visibility` of the cursor based on the state of the `inventory`.

The below example is the `ToggleInventory` function in the `PlayerInventory` script. First it finds whether the `inventory` is open or closed. The `isInventoryOpen` `bool` is set to `true` if it is closed and `false` if it is open. The `InventoryUI` `GameObject` then gets toggled on or off and the cursor is enabled/disabled based on if the `Inventory` is open or closed. Lastly, the players looking and interacting is toggled on or off as well.
```cs
public InventoryUI inventoryUI;
private FirstPersonController _fpc;

private void Start(){ _fpc = FindObjectOfType<FirstPersonController>(); }

public void ToggleInventory(InputAction.CallbackContext context)
{
    bool isInventoryOpen = !inventoryUI.gameObject.activeInHierarchy;
    inventoryUI.gameObject.SetActive(isInventoryOpen);
    SetCursorState(isInventoryOpen);

    _fpc.canLook = !isInventoryOpen;
    _fpc.canInteract = !isInventoryOpen;
}
```
