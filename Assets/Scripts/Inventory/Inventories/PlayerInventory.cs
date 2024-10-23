using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public Inventory PInventory;
    public InventoryUI inventoryUI;

    private FirstPersonController _fpc;

    private void Start()
    {
        PInventory = new Inventory(); 
        _fpc = FindObjectOfType<FirstPersonController>();
    }

    public void ToggleInventory(InputAction.CallbackContext context)
    {
        bool isInventoryOpen = !inventoryUI.gameObject.activeInHierarchy;
        inventoryUI.gameObject.SetActive(isInventoryOpen);
        SetCursorState(isInventoryOpen);
    
        _fpc.canLook = !isInventoryOpen;
        _fpc.canInteract = !isInventoryOpen;
    }

    private void SetCursorState(bool isInventoryOpen)
    {
        Cursor.visible = isInventoryOpen;
        Cursor.lockState = isInventoryOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public bool PickupItem(InventoryItem item)
    {
        bool added = PInventory.AddItem(item);
        inventoryUI.UpdateInventory(PInventory);
        return added;
    }

    public void DropItem(InventoryItem item)
    {
        PInventory.RemoveItem(item); 
        inventoryUI.UpdateInventory(PInventory);
        // Drop item in world v
    }
}
