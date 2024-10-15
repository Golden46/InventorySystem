using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputActions PlayerInputActions;

    private FirstPersonController _fpc;
    
    public static PlayerInputManager Instance;
    
    private void Awake()
    {
        if (Instance !=  null && Instance != this) Destroy(this);
        else Instance = this;

        PlayerInputActions ??= new PlayerInputActions();

        _fpc = FindObjectOfType<FirstPersonController>();
    }
    
    private void OnEnable()
    {
        PlayerInputActions.Player.Enable();
        PlayerInputActions.Player.Interact.performed += _fpc.HandleInteractionInput;
    }
    
    private void OnDisable()
    {
        PlayerInputActions.Player.Interact.performed -= _fpc.HandleInteractionInput;
        PlayerInputActions.Player.Disable();
    }
}
