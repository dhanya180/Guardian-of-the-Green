using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Animator))]  
public class EquipableItem : MonoBehaviour
{

    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) &&
            InventorySystem.Instance.isOpen == false &&
            CraftingSystem.instance.isOpen == false &&
            SelectionManager.Instance.handIsVisible == false
            )
        {
            animator.SetTrigger("hit");
        }
    }
}