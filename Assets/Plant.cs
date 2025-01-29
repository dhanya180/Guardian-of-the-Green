// using UnityEngine;
// using System.Collections;
// using System;

// using System.Collections.Generic;
// public class Plant : MonoBehaviour
// {
//      [SerializeField] GameObject seedModel;
//      [SerializeField] GameObject youngPlantModel;
//      [SerializeField] GameObject maturePlantModel;

//      [SerializeField] List<GameObject> plantProduceSpawns;

//      [SerializeField] GameObject producePrefab;

//      public int dayOfPlanting;
//      [SerializeField] int plantAge=0;

//      [SerializeField] int ageForYoungModel;
//      [SerializeField] int ageForMatureModel;
//      [SerializeField] int ageForFirstProduceBatch;

//      [SerializeField] int daysForNewProduce;
//      [SerializeField] int daysRemainingForNewProduceCounter;
//      [SerializeField] bool isOneTimeHarvest;
//      public bool isWatered;

//      private void OnEnable()
//      {

//         TimeManager.Instance.OnDayPass.AddListener(DayPass);
//      }

//      private void OnDisable()
//      {
//         TimeManager.Instance.OnDayPass.RemoveListener(DayPass);
//      }

//      private void OnDestroy()
//      {
//         GetComponentInParent<Soil>().isEmpty = true;
//         GetComponentInParent<Soil>().plantName="";
//         GetComponentInParent<Soil>().currentPlant=null;
//      }

//      private void DayPass()
//      {
//         if(isWatered)
//         {
//             plantAge++;
//             Debug.LogWarning(plantAge);
//             isWatered=false;
//             GetComponentInParent<Soil>().MakeSoilNotWatered();
//             GetComponent<SphereCollider>().enabled =false;
//         }
//         CheckGrowth();
//         if(!isOneTimeHarvest)
//         {
//             CheckProduce();
//         }
       
//      }

//      private void CheckGrowth()
//      {
//         seedModel.SetActive(plantAge < ageForYoungModel);
//         youngPlantModel.SetActive(plantAge >= ageForYoungModel && plantAge < ageForMatureModel);
//         maturePlantModel.SetActive(plantAge >= ageForMatureModel);

//         if(plantAge >= ageForMatureModel &&  isOneTimeHarvest)
//         {
//             MakePlantPickable();
//         }
//      }

//      private void  MakePlantPickable()
//      {
//         GetComponent<InteractableObject>().enabled=true;
//         GetComponent<SphereCollider>().enabled =true;
//      }
//      private void CheckProduce()
//      {
//         if(plantAge == ageForFirstProduceBatch)
//         {
//             GenerateProduceForEmptySpawns();
//         }
//         if(plantAge > ageForFirstProduceBatch)
//         {
//             if(daysRemainingForNewProduceCounter ==0)
//             {
//                 GenerateProduceForEmptySpawns();

//                 daysRemainingForNewProduceCounter = daysForNewProduce;
//             }
//             else
//             {
//                 daysRemainingForNewProduceCounter--;
//             }
//         }
//      }
//      private void GenerateProduceForEmptySpawns()
//      {
//         foreach(GameObject spawn in plantProduceSpawns)
//         {
//             if(spawn.transform.childCount==0)
//             {
//                 GameObject produce = Instantiate(producePrefab);
//                 produce.transform.parent = spawn.transform;
//                 Vector3 producePosition = Vector3.zero;
//                 producePosition.y=0f;
//                 produce.transform.localPosition = producePosition;
//             }
//         }
//      }
// }


using UnityEngine;
using System.Collections;
using System;

using System.Collections.Generic;
public class Plant : MonoBehaviour
{
     [SerializeField] GameObject seedModel;
     [SerializeField] GameObject youngPlantModel;
     [SerializeField] GameObject maturePlantModel;

     [SerializeField] List<GameObject> plantProduceSpawns;

     [SerializeField] GameObject producePrefab;

     public int dayOfPlanting;
     [SerializeField] int plantAge=0;

     [SerializeField] int ageForYoungModel;
     [SerializeField] int ageForMatureModel;
     [SerializeField] int ageForFirstProduceBatch;

     [SerializeField] int daysForNewProduce;
     [SerializeField] int daysRemainingForNewProduceCounter;
     [SerializeField] bool isOneTimeHarvest;
     public bool isWatered;

     private void OnEnable()
     {
        TimeManager.Instance.OnDayPass.AddListener(DayPass);
     }

     private void OnDisable()
     {
        TimeManager.Instance.OnDayPass.RemoveListener(DayPass);
     }

     private void OnDestroy()
     {
        GetComponentInParent<Soil>().isEmpty = true;
        GetComponentInParent<Soil>().plantName="";
        GetComponentInParent<Soil>().currentPlant=null;
     }

     private void DayPass()
     {
        if(isWatered)
        {
            plantAge++;
            isWatered=false;
            GetComponentInParent<Soil>().MakeSoilNotWatered();
            GetComponent<SphereCollider>().enabled =false;
        }
        CheckGrowth();
        if(!isOneTimeHarvest)
        {
            CheckProduce();
        }
       
     }

     private void CheckGrowth()
     {
        seedModel.SetActive(plantAge < ageForYoungModel);
        youngPlantModel.SetActive(plantAge >= ageForYoungModel && plantAge < ageForMatureModel);
        maturePlantModel.SetActive(plantAge >= ageForMatureModel);

        if(plantAge >= ageForMatureModel &&  isOneTimeHarvest)
        {
            MakePlantPickable();
        }
     }

     private void  MakePlantPickable()
     {
        GetComponent<InteractableObject>().enabled=true;
        GetComponent<SphereCollider>().enabled =true;
     }
     private void CheckProduce()
     {
        if(plantAge == ageForFirstProduceBatch)
        {
            GenerateProduceForEmptySpawns();
        }
        if(plantAge > ageForFirstProduceBatch)
        {
            if(daysRemainingForNewProduceCounter ==0)
            {
                GenerateProduceForEmptySpawns();

                daysRemainingForNewProduceCounter = daysForNewProduce;
            }
            else
            {
                daysRemainingForNewProduceCounter--;
            }
        }
     }
     private void GenerateProduceForEmptySpawns()
     {
        foreach(GameObject spawn in plantProduceSpawns)
        {
            if(spawn.transform.childCount==0)
            {
                GameObject produce = Instantiate(producePrefab);
                produce.transform.parent = spawn.transform;
                Vector3 producePosition = Vector3.zero;
                producePosition.y=0f;
                produce.transform.localPosition = producePosition;
            }
        }
     }
}