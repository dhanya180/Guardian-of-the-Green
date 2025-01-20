using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipSystem : MonoBehaviour
{
    public static EquipSystem Instance { get; set; }

    // -- UI -- //
    public GameObject quickSlotsPanel;

    public List<GameObject> quickSlotsList = new List<GameObject>();
    public List<string> itemList = new List<string>();

    public GameObject numbersHolder;

    public int selectedNumber = -1;
    public GameObject selectedItem;





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


    private void Start()
    {
        PopulateSlotList();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectQuickSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectQuickSlot(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectQuickSlot(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectQuickSlot(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectQuickSlot(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectQuickSlot(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectQuickSlot(7);
        }
    }

    //void SelectQuickSlot(int number)
    //{
    //    if (CheckIfSlotIsFull(number) == true)
    //    {
    //        if (selectedNumber != number)
    //        {
    //            selectedNumber = number;
    //            //Unselect previously item
    //            if (selectedItem != null)
    //            {
    //                selectedItem.gameObject.GetComponent<InventoryItem>().isSelected = false;
    //            }


    //            selectedItem = GetSelectedItem(number);
    //            selectedItem.GetComponent<InventoryItem>().isSelected = true;

    //            //changing the color

    //            foreach (Transform child in numbersHolder.transform)
    //            {
    //                child.transform.Find("Text").GetComponent<TextMeshProUGUI>().color = Color.gray;
    //            }


    //            TextMeshProUGUI toBeChanged = numbersHolder.transform.Find("Number" + number).transform.Find("Text").GetComponent<TextMeshProUGUI>();
    //            toBeChanged.color = Color.white;
    //        }
    //        else//we are trying to select the same slot
    //        {
    //            selectedNumber = -1;//null
    //            //Unselect previously item
    //            if (selectedItem != null)
    //            {
    //                selectedItem.gameObject.GetComponent<InventoryItem>().isSelected = false;
    //            }
    //            //changing the color

    //            foreach (Transform child in numbersHolder.transform)
    //            {
    //                child.transform.Find("Text").GetComponent<TextMeshProUGUI>().color = Color.gray;
    //            }
    //        }

    //    }

    //}


    void SelectQuickSlot(int number)
    {
        if (CheckIfSlotIsFull(number))
        {
            if (selectedNumber != number)
            {
                selectedNumber = number;

                // Unselect previously selected item
                if (selectedItem != null)
                {
                    selectedItem.GetComponent<InventoryItem>().isSelected = false;
                }

                selectedItem = GetSelectedItem(number);
                if (selectedItem != null)
                {
                    selectedItem.GetComponent<InventoryItem>().isSelected = true;
                }

                // Change all slot colors to gray
                //foreach (Transform child in numbersHolder.transform)
                //{
                //    var textComponent = child.Find("Text").GetComponent<TextMeshProUGUI>();
                //    if (textComponent != null)
                //    {
                //        textComponent.color = Color.gray;
                //    }
                //}
                foreach (Transform child in numbersHolder.transform)
                {
                    // Check if the "Text" child exists
                    Transform textChild = child.Find("text");
                    if (textChild != null)
                    {
                        TextMeshProUGUI textComponent = textChild.GetComponent<TextMeshProUGUI>();
                        if (textComponent != null)
                        {
                            textComponent.color = Color.gray;
                        }
                        else
                        {
                            Debug.LogWarning($"TextMeshProUGUI component not found on child '{child.name}'");
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"'Text' child not found under '{child.name}'");
                    }
                }


                // Change selected slot color to white
                var selectedText = numbersHolder.transform.Find("Number" + number)?.Find("Text").GetComponent<TextMeshProUGUI>();
                if (selectedText != null)
                {
                    Debug.Log($"Changing color of 'Number{number}' to white.");
                    selectedText.color = Color.white;
                    Debug.Log($"Color of 'Number{number}' is now {selectedText.color}.");

                    //selectedText.color = Color.white;
                }
                else
                {
                    Debug.LogWarning("Selected slot text not found for slot: " + number);
                }
            }
            else // Trying to select the same slot
            {
                selectedNumber = -1; // Reset selection

                // Unselect previously selected item
                if (selectedItem != null)
                {
                    selectedItem.GetComponent<InventoryItem>().isSelected = false;
                }

                // Reset all colors to gray
                foreach (Transform child in numbersHolder.transform)
                {
                    var textComponent = child.Find("Text")?.GetComponent<TextMeshProUGUI>();
                    if (textComponent != null)
                    {
                        textComponent.color = Color.gray;
                    }
                }
            }
        }
    }
    //void SelectQuickSlot(int number)
    //{
    //    var numberSlot = numbersHolder.transform.Find("Number" + number);
    //    if (numberSlot == null)
    //    {
    //        Debug.LogWarning($"Selected slot not found for slot: {number}");
    //        return;
    //    }

    //    var textObject = numberSlot.Find("Text");
    //    if (textObject == null)
    //    {
    //        Debug.LogWarning($"'Text' child not found under 'Number{number}'.");
    //        return;
    //    }

    //    var selectedText = textObject.GetComponent<TextMeshProUGUI>();
    //    if (selectedText == null)
    //    {
    //        Debug.LogWarning($"TextMeshProUGUI component not found on 'Text' child of 'Number{number}'.");
    //        return;
    //    }

    //    // Change color to white
    //    Debug.Log($"Changing color of 'Number{number}' text to white.");
    //    selectedText.color = Color.white;
    //}



    GameObject GetSelectedItem(int slotNumber)
    {
        return quickSlotsList[slotNumber - 1].transform.GetChild(0).gameObject;
    }




    bool CheckIfSlotIsFull(int slotNumber)
    {
        if (quickSlotsList[slotNumber - 1].transform.childCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }




    private void PopulateSlotList()
    {
        foreach (Transform child in quickSlotsPanel.transform)
        {
            if (child.CompareTag("QuickSlot"))
            {
                quickSlotsList.Add(child.gameObject);
            }
        }
    }

    public void AddToQuickSlots(GameObject itemToEquip)
    {
        // Find next free slot
        GameObject availableSlot = FindNextEmptySlot();
        // Set transform of our object
        itemToEquip.transform.SetParent(availableSlot.transform, false);
        // Getting clean name
        string cleanName = itemToEquip.name.Replace("(Clone)", "");
        // Adding item to list
        itemList.Add(cleanName);

        InventorySystem.Instance.ReCalculateList();

    }


    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }

    public bool CheckIfFull()
    {

        int counter = 0;

        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }
        }

        if (counter == 7)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}