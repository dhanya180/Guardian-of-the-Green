using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Apis.Admin.Economy;
using UnityEngine;
using UnityEngine.EventSystems;



public class ItemSlot : MonoBehaviour, IDropHandler
{

    public GameObject Item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }

            return null;
        }
    }






    //public void OnDrop(PointerEventData eventData)
    //{
    // Debug.Log("OnDrop");
    // Debug.Log($"OnDrop: Dropped item {DragDrop.itemBeingDragged.name} into {gameObject.name}");
    //if there is not item already then set our item.
    // if (!Item)
    //{
    //  DragDrop.itemBeingDragged.transform.SetParent(transform);
    //  DragDrop.itemBeingDragged.transform.localPosition = Vector2.zero;
    // Debug.Log($"Item {DragDrop.itemBeingDragged.name} set as child of {transform.name}");

    //var inventoryItem = DragDrop.itemBeingDragged.GetComponent<InventoryItem>();
    // if (inventoryItem != null)
    //  {
    // Handle QuickSlot-specific logic
    // if (transform.CompareTag("QuickSlot"))
    //  {
    //  inventoryItem.isInsideQuickSlot = true;
    // }
    // else
    // {
    //  inventoryItem.isInsideQuickSlot = false;
    // }
    // }
    // }
    //else
    // {
    //  Debug.LogWarning("Slot already occupied!");
    //}

    //StartCoroutine(DelayedScan());


    //}

    public void OnDrop(PointerEventData eventData)
{
    Debug.Log($"OnDrop: Dropped item {DragDrop.itemBeingDragged?.name} into {gameObject.name}");

    // Ensure the dragged item exists
    if (DragDrop.itemBeingDragged == null)
    {
        Debug.LogError("No item is being dragged!");
        return;
    }

    // Check if the slot is a SellSlot
    if (transform.CompareTag("SellSlot"))
    {
        Debug.Log($"Item {DragDrop.itemBeingDragged.name} dropped into Sell Panel slot: {gameObject.name}");

        // Reset the item's parent and position
        DragDrop.itemBeingDragged.transform.SetParent(transform);
        ResetRectTransform(DragDrop.itemBeingDragged.GetComponent<RectTransform>(), GetComponent<RectTransform>());

        // Trigger sell logic
        var inventoryItem = DragDrop.itemBeingDragged.GetComponent<InventoryItem>();
        if (inventoryItem != null)
        {
            SellItem(inventoryItem);
        }
        return; // Exit after handling SellSlot
    }

    // Handle QuickSlot and Regular Slots
    if (!Item) // Check if the slot is empty
    {
        DragDrop.itemBeingDragged.transform.SetParent(transform);
        ResetRectTransform(DragDrop.itemBeingDragged.GetComponent<RectTransform>(), GetComponent<RectTransform>());

        Debug.Log($"Item {DragDrop.itemBeingDragged.name} set as child of {transform.name}");

        // Handle QuickSlot-specific logic
        var inventoryItem = DragDrop.itemBeingDragged.GetComponent<InventoryItem>();
        if (inventoryItem != null)
        {
            inventoryItem.isInsideQuickSlot = transform.CompareTag("QuickSlot");
            Debug.Log($"Item {inventoryItem.name} is {(inventoryItem.isInsideQuickSlot ? "inside" : "outside")} a QuickSlot.");
        }
    }
    else
    {
        Debug.LogWarning($"Slot {gameObject.name} is already occupied!");
    }

    // Update inventory or UI
    StartCoroutine(DelayedScan());
}

// Method to Reset RectTransform
private void ResetRectTransform(RectTransform itemRect, RectTransform slotRect)
{
    itemRect.anchorMin = new Vector2(0.5f, 0.5f);
    itemRect.anchorMax = new Vector2(0.5f, 0.5f);
    itemRect.pivot = new Vector2(0.5f, 0.5f);
    itemRect.anchoredPosition = Vector2.zero;
    itemRect.sizeDelta = slotRect.sizeDelta;
}

// Example SellItem Method
private void SellItem(InventoryItem item)
{
    Debug.Log($"Selling item: {item.name}");
        // Add currency, remove from inventory, etc.
        InventorySystem.Instance.currentCoins += item.amountInInventory * item.sellingPrice;

        // Remove the item from the inventory
       // InventorySystem.Instance.RemoveItem(item);

        // Optionally destroy the item GameObject
        Destroy(item.gameObject);

        // Update the UI
        SellSystem.Instance.ScanItemsInSlots();
        SellSystem.Instance.UpdateSellAmountUI();
    }


    IEnumerator DelayedScan()
    {
        yield return new WaitForSeconds(0.1f);
        SellSystem.Instance.ScanItemsInSlots();
        SellSystem.Instance.UpdateSellAmountUI();
    }
}