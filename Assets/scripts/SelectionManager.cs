using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;
using UnityEditor;
using Microsoft.Unity.VisualStudio.Editor;


public class SelectionManager : MonoBehaviour

{

    public static SelectionManager Instance { get; private set; }
    public RectTransform pointerTransform;
    public bool onTarget;
    
    public GameObject interaction_Info_UI;
    TextMeshProUGUI interaction_text;

    public Image centerDotImage;
    public Image handIcon;

    public bool handIsVisible;

    public GameObject selectedSoil;

    private void Start()
    {
        onTarget = false;
        interaction_text = interaction_Info_UI.GetComponent<TextMeshProUGUI>();
    }

    //void Update()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;
    //    if (Physics.Raycast(ray, out hit))
    //    {
    //        var selectionTransform = hit.transform;

    //        if (selectionTransform.GetComponent<InteractableObject>())
    //        {
    //            interaction_text.text = selectionTransform.GetComponent<InteractableObject>().GetItemName();
    //            interaction_Info_UI.SetActive(true);
    //        }
    //        else
    //        {
    //            interaction_Info_UI.SetActive(false);
    //        }

    //    }
    //    else
    //    {
    //        interaction_Info_UI.SetActive(false);
    //    }
    //}




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




    void Update()
    {
        // Convert pointer position to screen position
        Vector2 pointerScreenPosition = RectTransformUtility.WorldToScreenPoint(null, pointerTransform.position);

        // Cast a ray from the pointer's position
        Ray ray = Camera.main.ScreenPointToRay(pointerScreenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;

            InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>();

            if (interactable)
            {
                onTarget = true;
                interaction_text.text = interactable.GetItemName();
                interaction_Info_UI.SetActive(true);
                /*if ()
                {

                }
                else
                {

                }*/
            }
            else
            {
                onTarget = false;
                interaction_Info_UI.SetActive(false);
                handIsVisible = false;
            }

            Soil soil = selectionTransform.GetComponent<Soil>();

            if(soil && soil.playerInRange)
            {
                if(soil.isEmpty && EquipSystem.Instance.IsPlayerHoldingSeed())
                {
                    string seedName = EquipSystem.Instance.selectedItem.GetComponent<InventoryItem>().thisName;
                    string onlyPlantName = seedName.Split(new string[] { "Seed"},StringSplitOptions.None)[0];

                    interaction_text.text = "Plant"+  onlyPlantName;
                    interaction_Info_UI.SetActive(true);

                    if(Input.GetMouseButtonDown(0))
                    {
                        soil.PlantSeed();
                        Destroy(EquipSystem.Instance.selectedItem);
                        Destroy(EquipSystem.Instance.selectedItemModel);
                    }

                }
                else if(soil.isEmpty)
                {
                    interaction_text.text="Soil";
                    interaction_Info_UI.SetActive(true);
                }
                else
                {
                    if(EquipSystem.Instance.IsPlayerHoldingWateringCan())
                    {
                        if(soil.currentPlant.isWatered)
                        {
                            interaction_text.text=soil.plantName;
                        interaction_Info_UI.SetActive(true);
                        }
                        else
                        {
                            interaction_text.text="Use Watering Can";
                        interaction_Info_UI.SetActive(true);

                        if(Input.GetMouseButtonDown(0))
                        {
                            soil.currentPlant.isWatered =true;
                            soil.MakeSoilWatered();
                        }
                        }
                    }
                    else
                    {
                        interaction_text.text=soil.plantName;
                        interaction_Info_UI.SetActive(true);
                    }

                }

                selectedSoil = soil.gameObject;
            }
            else
            {
                if(selectedSoil != null)
                {
                    selectedSoil  =null;
                }

            }

            if( !soil)
            {
                if(!interactable){
                interaction_Info_UI.SetActive(false);
                interaction_text.text ="";
                }
            }
        }
       else
        {
            onTarget = false;
            interaction_Info_UI.SetActive(false);
            handIsVisible = false;
        }
    }

    public void DisableSelection()
    {
        interaction_Info_UI.SetActive(false);
        pointerTransform .gameObject.SetActive(false);  
        //handicon
        //centerdotImage
        
    }

    public void EnableSelection()
    {
        interaction_Info_UI.SetActive(true);
        pointerTransform .gameObject.SetActive(true);
        //handicon
        //centerdotImage
    }






}   