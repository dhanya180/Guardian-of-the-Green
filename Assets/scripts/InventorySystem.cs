using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySystem : MonoBehaviour
{


   

    public static InventorySystem Instance { get; private set; }

    public GameObject inventoryScreenUI;

    public List<GameObject> slotList = new List<GameObject>();

    public List<string> itemList = new List<string>();

    private GameObject itemToAdd;

    private GameObject whatSlotToEquip;
    public GameObject ItemInfoUI;


    //public bool isFull;
    public bool isOpen;

    //Pickup Popup
    public GameObject pickupAlert;
    public TextMeshProUGUI pickupName;
    public Image pickupImage;

    


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {

        isOpen = false;
       
        PopulateSlotList();
        Cursor.visible = false;

    }


    private void PopulateSlotList()
    {
        foreach (Transform child in inventoryScreenUI.transform)
        {
            if (child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }






    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {

            Debug.Log("i is pressed");
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SelectionManager.Instance.DisableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = false;

            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            if (!CraftingSystem.instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                SelectionManager.Instance.EnableSelection();
                SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;

            }
           
            isOpen = false;
        }
    }

    public void AddToInventory(string itemName)
    {
        
            whatSlotToEquip = FindNextEmptySlot();

            itemToAdd = Instantiate(Resources.Load<GameObject>(itemName) , whatSlotToEquip.transform.position , whatSlotToEquip.transform.rotation);
            itemToAdd.transform.SetParent(whatSlotToEquip.transform);

            itemList.Add(itemName);


            TriggerPickupPopUp(itemName,itemToAdd.GetComponent<Image>().sprite);


            ReCalculateList();
            CraftingSystem.instance.RefreshNeededItems();

            
        
    }
    






    void TriggerPickupPopUp(string itemName,Sprite itemSprite )
    {
        pickupAlert.SetActive(true);
        pickupName.text = itemName;
        pickupImage.sprite = itemSprite;

        // Start the coroutine to hide the alert after 2 seconds
        StartCoroutine(HidePickupAlertAfterDelay(0.7f));
    }

    IEnumerator HidePickupAlertAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        pickupAlert.SetActive(false);
    }




    public bool CheckIfFull()
    {
        int counter = 0;
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }
        }
            if(counter == 24)
            {
                return true;
            }
            else
            {
                return false;   
            }
    }
    

    public GameObject FindNextEmptySlot()
    {
        foreach(GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }

    public void RemoveItem(string nameToRemove , int amountToRemove)
    {
        int counter = amountToRemove;

        for (var i = slotList.Count - 1;i>=0;i--)
        {
            if (slotList[i].transform.childCount > 0)
            {
                if (slotList[i].transform.GetChild(0).name == nameToRemove + "(Clone)" && counter!=0)
                {
                    Destroy(slotList[i].transform.GetChild(0).gameObject);
                    counter--;
                }
            }
            if(counter == 0)
            {
                break;
            }
        }

        ReCalculateList();
        CraftingSystem.instance.RefreshNeededItems();
    }

    public void ReCalculateList()
    {
        itemList.Clear();

        foreach (GameObject slot in slotList)
        {
            if(slot.transform.childCount > 0)
            {
                string name = slot.transform.GetChild(0).name;
                string str1 = name;
                string str2 = "(Clone)";

                string result = str1 .Replace(str2, "");


                itemList.Add(result);
            }
        }
    }



}