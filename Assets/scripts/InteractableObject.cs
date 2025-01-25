/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string ItemName;

    public string GetItemName()
    {
        return ItemName;
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0) && SelectionManager.Instance.onTarget)
        {
            Debug.Log("item added to inventory");

            Destroy(gameObject);
        }
        }
        //void Update()
        //{
        //    // Convert the pointer's UI position to screen position
        //    Vector2 pointerScreenPosition = RectTransformUtility.WorldToScreenPoint(null, SelectionManager.Instance.pointerTransform.position);

        //    // Cast a ray from the pointer's position
        //    Ray ray = Camera.main.ScreenPointToRay(pointerScreenPosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        var selectionTransform = hit.transform;

        //        // Check if the ray hits the stone
        //        if (selectionTransform.CompareTag("Stone")) // Make sure the stone is tagged as "Stone"
        //        {
        //            Debug.Log("Pointer is on the stone!");

        //            // Trigger interaction logic
        //            if (Input.GetKeyDown(KeyCode.E)) // Interaction key, can be changed
        //            {
        //                Debug.Log("Stone removed!");
        //                Destroy(selectionTransform.gameObject); // Destroy the stone
        //            }
        //        }
        //    }
        //}


    }*/

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string ItemName;

    public string GetItemName()
    {
        return ItemName;
    }

    void Update()
    {
        // Check if the pointer is over this object
        if (SelectionManager.Instance.onTarget)
        {
            // Check for interaction trigger
            if (Input.GetKeyDown(KeyCode.Mouse0)) // Replace Mouse0 with other logic if needed
            {
                Debug.Log($"Picked up: {ItemName}");
                Destroy(gameObject); // Destroy the object or handle pickup logic
            }
        }
    }
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string ItemName;
    public bool playerInRange;

    [SerializeField] float detectionRange=10f;

    public string GetItemName()
    {
        return ItemName;
    }

    void Update()
    {

        float distance = Vector3.Distance(PlayerState.Instance.playerBody.transform.position,transform.position);
        if(distance < detectionRange)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
       // Get the pointer's screen position from the SelectionManager
        Vector2 pointerScreenPosition = RectTransformUtility.WorldToScreenPoint(
            null,
            SelectionManager.Instance.pointerTransform.position
        );

        // Cast a ray from the pointer's screen position
        Ray ray = Camera.main.ScreenPointToRay(pointerScreenPosition);
        RaycastHit hit;

        // Check if the ray hits this object
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform) // Ensure the ray hits this object
            {
                Debug.Log($"Pointer is over {ItemName}");

                // Trigger interaction logic when pointer is over the object
                if (Input.GetKeyDown(KeyCode.E)) // Example: Press 'E' to interact
                {
                    //if inventory is NOT full
                    if (!InventorySystem.Instance.CheckIfFull())
                    {
                        InventorySystem.Instance.AddToInventory(ItemName);
                        InventorySystem.Instance.itemsPickedup.Add(gameObject.name);

                        Debug.Log($"Picked up: {ItemName}");
                        Destroy(gameObject); // Destroy the object or handle custom logic
                    }
                    else
                    {
                        Debug.Log("inventory is full");
                    }
                }
            }
        }
    }
}
