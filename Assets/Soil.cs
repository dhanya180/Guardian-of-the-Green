// using UnityEngine;
// using System;


// public class Soil : MonoBehaviour
// {
//     public bool isEmpty =true;
//     public bool playerInRange;
//     public string plantName;
//     public Plant currentPlant;
//     public Material defaultMaterial;
//     public Material wateredMaterial;


//     private void Update()
//     {
//         float distance = Vector3.Distance(PlayerState.Instance.playerBody.transform.position,transform.position);
//         {
//             if(distance <10f )
//             {
//                 playerInRange = true;
//             }
//             else
//             {
//                 playerInRange=false;
//             }
//         }
//     }

//     internal void PlantSeed()
//     {
//         InventoryItem selectedSeed = EquipSystem.Instance.selectedItem.GetComponent<InventoryItem>();
//         isEmpty=false;



//         string onlyPlantName = selectedSeed.thisName.Split(new string[] { "Seed"},StringSplitOptions.None)[0];
//         plantName = onlyPlantName;
//         GameObject instantiatedPlant = Instantiate(Resources.Load($"{onlyPlantName}Plant") as GameObject);
//         instantiatedPlant.transform.parent=gameObject.transform;
//         Vector3 plantPosition = Vector3.zero;
//         plantPosition.y=0f;
//         instantiatedPlant.transform.localPosition = plantPosition;
//         currentPlant = instantiatedPlant.GetComponent<Plant>();
//         currentPlant.dayOfPlanting = TimeManager.Instance.dayInGame;
//     }

//     internal void MakeSoilWatered()
//     {
//         GetComponent<Renderer>().material = wateredMaterial;
//     }

//     internal void MakeSoilNotWatered()
//     {
//         GetComponent<Renderer>().material = defaultMaterial;
//     }
// }



using UnityEngine;
using System;

public class Soil : MonoBehaviour
{
    public bool isEmpty = true; // Indicates if the soil is empty
    public bool playerInRange; // Checks if the player is in range
    public string plantName; // Name of the plant in this soil
    public Plant currentPlant; // Reference to the current plant
    public Material defaultMaterial; // Default soil material
    public Material wateredMaterial; // Material for watered soil

    private GameManager gameManager; // Reference to the GameManager

    private void Start()
    {
        // Find and cache the GameManager instance
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        // Calculate distance between the player and the soil
        float distance = Vector3.Distance(PlayerState.Instance.playerBody.transform.position, transform.position);

        // Check if the player is in range
        playerInRange = distance < 10f;
    }

    internal void PlantSeed()
    {
        // Get the selected seed from the equip system
        InventoryItem selectedSeed = EquipSystem.Instance.selectedItem.GetComponent<InventoryItem>();
        isEmpty = false; // Set soil to not empty

        // Extract plant name from the seed name
        string onlyPlantName = selectedSeed.thisName.Split(new string[] { "Seed" }, StringSplitOptions.None)[0];
        plantName = onlyPlantName;

        // Instantiate the plant in the soil
        GameObject instantiatedPlant = Instantiate(Resources.Load($"{onlyPlantName}Plant") as GameObject);
        instantiatedPlant.transform.parent = gameObject.transform;
        instantiatedPlant.transform.localPosition = Vector3.zero;

        // Initialize plant properties
        currentPlant = instantiatedPlant.GetComponent<Plant>();
        currentPlant.dayOfPlanting = TimeManager.Instance.dayInGame;

        // Increase oxygen level in the GameManager
        if (gameManager != null)
        {
            gameManager.IncreaseOxygen(10f);
        }
    }

    internal void MakeSoilWatered()
    {
        // Change material to watered material
        GetComponent<Renderer>().material = wateredMaterial;
    }

    internal void MakeSoilNotWatered()
    {
        // Change material to default material
        GetComponent<Renderer>().material = defaultMaterial;
    }
}