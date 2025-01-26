using System;
using System.Collections;
using System.Collections.Generic;


using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugManager;

public class ShopKeeper : MonoBehaviour
{
    public bool playerInRange;
    public bool isTalkingWithPlayer;


    public GameObject shopKeeperDialogUI;
    public Button buyBtn;
    public Button sellBtn;
    public Button exitBtn;
    public GameObject buyPanelUI;
    public GameObject sellPanelUI;


    private void Start()
    {
       shopKeeperDialogUI.SetActive(false);
        buyBtn.onClick.AddListener(BuyMode);
        sellBtn.onClick.AddListener(SellMode);
        exitBtn.onClick.AddListener(StopTalking);

    }

    private void BuyMode()
    {
       sellPanelUI.SetActive(false);
        buyPanelUI.SetActive(true);
        HideDialogUI();
    }

    private void SellMode()
    {
        sellPanelUI.SetActive(true);
        buyPanelUI.SetActive(false);
        HideDialogUI();
    }

    public void DialogMode()
    {
        sellPanelUI.SetActive(false);
        buyPanelUI.SetActive(false);
        DisplayDialogUI();
    }
    public void Talk()
    {
         isTalkingWithPlayer = true;
        DisplayDialogUI();

        MovementManager.Instance.EnableMovement(false);
        MovementManager.Instance.EnableLook(false);

        Cursor.lockState= CursorLockMode.None;
        Cursor.visible = true;
    }

  

    public void StopTalking()
    {
        isTalkingWithPlayer = false;
        HideDialogUI();
        MovementManager.Instance.EnableMovement(true);
        MovementManager.Instance.EnableLook(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void DisplayDialogUI()
    {
        shopKeeperDialogUI.SetActive(true);
    }
    private void HideDialogUI()
    {
        shopKeeperDialogUI.SetActive(false );
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Entered Trigger with: {other.gameObject.name}"); // Debug the name of the object entering
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered range"); // Confirm the player is detected
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"Exited Trigger with: {other.gameObject.name}"); // Debug the name of the object exiting
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited range"); // Confirm the player exits
            playerInRange = false;
        }
    }
}
