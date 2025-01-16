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
            }
            else
            {
                onTarget = false;
                interaction_Info_UI.SetActive(false);
            }
        }
        else
        {
            onTarget = false;
            interaction_Info_UI.SetActive(false);
        }
    }

}