using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform inventoryPanel;

    public List<InventorySlot> inventorySlots;

    public void UpdateInventory(Inventory playerInventory)
    {
        foreach (Transform child in inventoryPanel) Destroy(child.gameObject);

        foreach (var item in playerInventory.Items)
        {
            GameObject newSlot = Instantiate(slotPrefab, inventoryPanel);
            InventorySlot slotComponent = newSlot.GetComponent<InventorySlot>();
            slotComponent.SetItem(item);
        }
    }

    public void SwapItems(InventorySlot fromSlot, InventorySlot toSlot)
    {
        InventoryItem fromItem = fromSlot.currentItem;
        fromSlot.SetItem(toSlot.currentItem);
        toSlot.SetItem(fromItem);
    }
}