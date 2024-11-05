using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryItem currentItem;
    public Image icon;
    public string itemStats;

    [SerializeField] private GameObject statPanel;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI stats;

    private PlayerInventory _playerInventory;
    private Transform _playerTransform;
    private Transform _originalParent;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _playerInventory = FindObjectOfType<PlayerInventory>();
        _canvasGroup = icon.GetComponent<CanvasGroup>();
        _playerTransform = GameObject.FindWithTag("Player").transform;
    }

    public void SetItem(InventoryItem item)
    {
        currentItem = item;
        icon.sprite = item.itemIcon;

        itemName.text = item.name;
        SetItemStats(currentItem);
    }

    private void SetItemStats(InventoryItem item)
    {
        itemStats = "";
        foreach (KeyValuePair<string, int> kvp in item.ItemStats()) itemStats += $"{kvp.Key}: {kvp.Value}\n";
        stats.text = itemStats;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnPointerExit(eventData);

        _originalParent = icon.transform.parent;
        icon.transform.SetParent(transform.root);
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        icon.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        icon.transform.SetParent(_originalParent);
        icon.transform.localPosition = Vector3.zero;
        _canvasGroup.blocksRaycasts = true;

        if (!RectTransformUtility.RectangleContainsScreenPoint((RectTransform)transform.parent, eventData.position))
        {
            DropItemInWorld();
            Destroy(gameObject);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        InventorySlot draggedSlot = eventData.pointerDrag.GetComponent<InventorySlot>();

        if (draggedSlot != null && draggedSlot != this)
        {
            InventoryUI inventoryUI = GetComponentInParent<InventoryUI>();
            inventoryUI.SwapItems(draggedSlot, this);
        }
    }

    private void DropItemInWorld()
    {
        Vector3 dropPosition = _playerTransform.position + _playerTransform.right * 2;
        _playerInventory.DropItem(currentItem);
        Instantiate(currentItem.prefab, dropPosition, Quaternion.identity);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        statPanel.SetActive(true);
        statPanel.transform.SetParent(transform.parent);
        statPanel.transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        statPanel.SetActive(false);
        statPanel.transform.SetParent(transform);
    }
}