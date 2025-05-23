using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CraftingSystem : MonoBehaviour
{

    public GameObject craftingScreenUI;
    public GameObject toolsScreenUI;

    public List<string> inventoryItemList = new List<string>();

    //category buttons
    Button toolsBTN;

    //craft buttons
    Button craftAxeBTN;

    //Requirement Text
    TextMeshProUGUI AxeReq1;
    TextMeshProUGUI AxeReq2;

    public bool isOpen;

    //All blueprint
    public Blueprint AxeBLP = new Blueprint("Axe", 2 , "Stone" , 3 , "Stick" , 3);




    public static CraftingSystem instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }







    //start is called before the first frame update
    void Start()
    {

        isOpen = false;
        toolsBTN = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate { OpenToolsCategory(); });
        //AXE
        AxeReq1 = toolsScreenUI.transform.Find("Axe").transform.Find("req1").GetComponent<TextMeshProUGUI>();
        AxeReq2 = toolsScreenUI.transform.Find("Axe").transform.Find("req2").GetComponent<TextMeshProUGUI>();


        craftAxeBTN = toolsScreenUI.transform.Find("Axe").transform.Find("Button").GetComponent <Button>();
        craftAxeBTN.onClick.AddListener(delegate { CraftAnyItem(AxeBLP); });
    }


    void OpenToolsCategory()
    {
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(true);
    }


    //update is called once per frame
    void Update()
    {

        //RefreshNeededItems();

        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {


            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SelectionManager.Instance.DisableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled=false;


            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolsScreenUI.SetActive(false);
            if (!InventorySystem.Instance.isOpen)
            { 
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            SelectionManager.Instance.EnableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;
            }
            isOpen = false;
        }
    }

    void CraftAnyItem(Blueprint blueprintToCraft)
    {
        //add item to inventory
        InventorySystem.Instance.AddToInventory(blueprintToCraft.itemName);

        //remove resources from inventory
        if(blueprintToCraft.numOfRequirements == 1)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
        }
        else if(blueprintToCraft.numOfRequirements == 2){
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req2, blueprintToCraft.Req2amount);
        }

        //refresh list
        StartCoroutine(Calculate());


        //RefreshNeededItems();
    }
    
    public IEnumerator Calculate()
    {
        yield return 0;

        InventorySystem.Instance.ReCalculateList();
        RefreshNeededItems();
    }






    public void RefreshNeededItems()
    {
        int stone_count = 0;
        int stick_count = 0;
        inventoryItemList = InventorySystem.Instance.itemList;

        foreach (string itemName in inventoryItemList)
        {
            switch (itemName)
            {
                case "Stone":
                    stone_count++;
                    break;

                case "Stick":
                    stick_count++;
                    break;
            }
        }
        //---AXE---
        if (AxeReq1 != null)
        {
            AxeReq1.text = "3 Stone [" + stone_count + "]";
        }
        if (AxeReq2 != null)
        {
            AxeReq2.text = "3 Stick [" + stick_count + "]";
        }

        if (stone_count>=3 && stick_count>= 3)
        {
            craftAxeBTN.gameObject.SetActive(true);
        }
        else
        {
            craftAxeBTN.gameObject.SetActive(false);
        }
    }




}