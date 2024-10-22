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
        if (inventoryUI.gameObject.activeInHierarchy)
        {
            inventoryUI.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _fpc.canLook = true;
            _fpc.canInteract = true;
        }
        else
        {
            _fpc.canLook = false;
            _fpc.canInteract = false;
            inventoryUI.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; 
        }
            
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
