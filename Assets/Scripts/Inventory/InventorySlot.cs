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

    private Transform playerTransform;
    private Transform originalParent;
    private CanvasGroup canvasGroup;
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = icon.GetComponent<CanvasGroup>();
        playerTransform = GameObject.FindWithTag("Player").transform;
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

        originalParent = icon.transform.parent;
        icon.transform.SetParent(transform.root);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        icon.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        icon.transform.SetParent(originalParent);
        icon.transform.localPosition = Vector3.zero;
        canvasGroup.blocksRaycasts = true;

        if (!RectTransformUtility.RectangleContainsScreenPoint((RectTransform)transform.parent, eventData.position))
        {
            DropIteminWorld();
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

    private void DropIteminWorld()
    {
        Vector3 dropPosition = playerTransform.position + playerTransform.right * 2;

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