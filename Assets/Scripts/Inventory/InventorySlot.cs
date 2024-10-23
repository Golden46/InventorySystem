using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    private InventoryItem _currentItem;

    [SerializeField] private GameObject statPanel;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI stats;

    public void SetItem(InventoryItem item)
    {
        _currentItem = item;
        icon.sprite = item.itemIcon;
        itemName.text = item.name;

        string itemStats = "";
        foreach (KeyValuePair<string, int> kvp in _currentItem.ItemStats()) itemStats += $"{kvp.Key}: {kvp.Value}\n";
        stats.text = itemStats;
    }

    public void OnSlotClick()
    {
        Debug.Log($"Clicked on {_currentItem.itemName}");
    }

    public void OnPointerEnter()
    {
        statPanel.SetActive(true);
        statPanel.transform.SetParent(transform.parent);
        statPanel.transform.SetAsLastSibling();
    }

    public void OnPointerExit()
    {
        statPanel.SetActive(false);
        statPanel.transform.SetParent(transform);
    }
}
