using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputActions PlayerInputActions;

    private FirstPersonController _fpc;
    private PlayerInventory _pi;

    private void Awake()
    {
        PlayerInputActions ??= new PlayerInputActions();

        _fpc = FindObjectOfType<FirstPersonController>();
        _pi = FindObjectOfType<PlayerInventory>();
    }
    
    private void OnEnable()
    {
        PlayerInputActions.Player.Enable();
        PlayerInputActions.Player.Interact.performed += _fpc.HandleInteractionInput;
        PlayerInputActions.Player.Inventory.performed += _pi.ToggleInventory;
    }
    
    private void OnDisable()
    {
        PlayerInputActions.Player.Interact.performed -= _fpc.HandleInteractionInput;
        PlayerInputActions.Player.Inventory.performed -= _pi.ToggleInventory;
        PlayerInputActions.Player.Disable();
    }
}
