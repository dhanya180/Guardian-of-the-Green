using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;
using UnityEditor;


public class SelectionManager : MonoBehaviour

{

    public static SelectionManager Instance { get; private set; }
    public RectTransform pointerTransform;
    public bool onTarget;
    
    public GameObject interaction_Info_UI;
    TextMeshProUGUI interaction_text;

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
        // Ensure pointerTransform is assigned
        if (pointerTransform == null)
        {
            Debug.LogError("Pointer Transform is not assigned!");
            return;
        }

        // Convert pointer position to screen position
        Vector2 pointerScreenPosition = RectTransformUtility.WorldToScreenPoint(null, pointerTransform.position);

        // Cast a ray from the pointer's position
        Ray ray = Camera.main.ScreenPointToRay(pointerScreenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;

            // Check if the object has a Soil component
            Soil soil = selectionTransform.GetComponent<Soil>();

            if (soil != null && soil.playerInRange)
            {
                onTarget = true;

                if (soil.isEmpty)
                {
                    interaction_text.text = "Soil";
                }
                else
                {
                    interaction_text.text = "Name of plant";
                }

                interaction_Info_UI.SetActive(true);
                selectedSoil = soil.gameObject;
            }
            else
            {
                // If not soil, check for other interactable objects
                InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>();

                if (interactable != null)
                {
                    onTarget = true;
                    interaction_text.text = interactable.GetItemName();
                    interaction_Info_UI.SetActive(true);
                }
                else
                {
                    ResetInteraction();
                }
            }
        }
        else
        {
            ResetInteraction();
        }
    }

    private void ResetInteraction()
    {
        onTarget = false;
        interaction_Info_UI.SetActive(false);
        interaction_text.text = "";
        selectedSoil = null;
    }
}