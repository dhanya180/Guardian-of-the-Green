using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static BuySystem;

public class ShopItemSlot : MonoBehaviour
{
    [Header("UI")]
    public Image itemImageUI;
    public TextMeshProUGUI itemNameUI;
    public TextMeshProUGUI itemPriceUI;
    public Button buyButtonUI;

    [Header("Data")]
    public ShopItemData shopItemData;

    private void Start()
    {
        buyButtonUI.onClick.AddListener(BuyItem);
    }

    
    void Update()
    {
       // int currentMoney = 100; 
         int currentMoney = InventorySystem.Instance.currentCoins;

        
        if (shopItemData.itemPrice <= currentMoney) 
        {
            buyButtonUI.interactable = true;
        }
        else
        {
            buyButtonUI.interactable = false;
        }
    }

    public void BuyItem()
    {
        
      InventorySystem.Instance.currentCoins -= shopItemData.itemPrice;

        
        InventoryItem inventoryItem = shopItemData.inventoryItem.GetComponent<InventoryItem>();

        
        InventorySystem.Instance.AddToInventory(inventoryItem.thisName);

        Debug.Log("Bought " + inventoryItem.thisName);
    }
}
