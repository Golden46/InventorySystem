# PlayerInventory / ToggleInventory

### Declaration
public void ToggleInventory(InputAction.CallbackContext context)

### Returns
`none`

### Description
Open and closes the player inventory.

The below example creates a new ```PlayerInputActions``` ```instance``` if it doesn't already exist and gets a reference to the `PlayerInventory` script. Then it enables the `Player` action map and subscribes to ToggleInventory function as an event. The function will run every time the `Inventory` key in the `Player` action map is pressed.
```cs
public static PlayerInputActions PlayerInputActions;
private PlayerInventory _pi;

private void Awake()
{
    PlayerInputActions ??= new PlayerInputActions();
    _pi = FindObjectOfType<PlayerInventory>();
}

private void OnEnable()
{
    PlayerInputActions.Player.Enable();
    PlayerInputActions.Player.Inventory.performed += _pi.ToggleInventory;
}
    
private void OnDisable()
{
    PlayerInputActions.Player.Inventory.performed -= _pi.ToggleInventory;
    PlayerInputActions.Player.Disable();
}
```
