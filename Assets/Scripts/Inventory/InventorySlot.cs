using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    private InventoryItem _currentItem;

    public void SetItem(InventoryItem item)
    {
        _currentItem = item;
        icon.sprite = item.itemIcon;
    }

    public void OnSlotClick()
    {
        Debug.Log($"Clicked on {_currentItem.itemName}");
    }
}
