using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    private Inventory _pInventory;
    public InventoryUI inventoryUI;

    private FirstPersonController _fpc;

    private void Start()
    {
        _pInventory = new Inventory(); 
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
        bool added = _pInventory.AddItem(item);
        inventoryUI.UpdateInventory(_pInventory);
        return added;
    }

    public void DropItem(InventoryItem item)
    {
        _pInventory.RemoveItem(item); 
        inventoryUI.UpdateInventory(_pInventory);
    }

    public void SwapItem(InventoryItem item)
    {
        
    }
}
