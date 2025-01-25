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
            Transform selectionTransform = hit.transform;
            Debug.Log($"Raycast hit: {selectionTransform.name}"); // Debug the hit object name

            // Handle shopkeeper logic
            ShopKeeper shop = selectionTransform.GetComponent<ShopKeeper>();
            if (shop)
            {
                Debug.Log("Shopkeeper detected.");
                Debug.Log($"Player in range: {shop.playerInRange}");


                if (shop.playerInRange)
                {
                    interaction_text.text = "Talk";
                    interaction_Info_UI.SetActive(true);

                    if (Input.GetMouseButtonDown(0) && !shop.isTalkingWithPlayer)
                    {
                        Debug.Log("Player clicked to talk with shopkeeper.");
                        shop.Talk(); // Initiate dialogue with shopkeeper
                    }
                }
                else
                {
                    interaction_text.text = "";
                    interaction_Info_UI.SetActive(false);
                }

                return; // Exit here to avoid further checks since the shopkeeper logic is handled
            }

            // Handle generic interactable objects
            InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>();
            if (interactable)
            {
                onTarget = true;
                interaction_text.text = interactable.GetItemName();
                interaction_Info_UI.SetActive(true);

                // Additional logic for interactable objects can go here
            }
            else
            {
                onTarget = false;
                interaction_Info_UI.SetActive(false);
                handIsVisible = false;
            }
        }
        else
        {
            // Raycast did not hit any object
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