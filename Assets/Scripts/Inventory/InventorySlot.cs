using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    private InventoryItem _currentItem;
    private bool _isHovering;

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

    public void OnDrag()
    {
        OnPointerExit();
        icon.color = Color.clear;
    }

    public void OnDragEnd()
    {
        OnPointerEnter();
        icon.color = Color.white;
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
