using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance { get; private set; }

    //Player Health
    public float currentHealth;
    public float maxHealth;
    //player calories
    public float currentCalories;
    public float maxCalories;

    //player hydration
    public float currentHydrationPercent;
    public float maxHydrationPercent;

    float distanceTravelled=0;
    Vector3 lastPosition;
    public GameObject playerBody;




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

    void Start()
    {
        currentHealth = maxHealth; 
        currentCalories = maxCalories;
        currentHydrationPercent = maxHydrationPercent;

    }
    void Update()
    {
        distanceTravelled+=Vector3.Distance(playerBody.transform.position,lastPosition);
        lastPosition =playerBody.transform.position;
        if(distanceTravelled >=5)
        {
            distanceTravelled=0;
            currentCalories-=1;
        }



        if (Input.GetKeyDown(KeyCode.N))
        {
            currentHealth -= 10;
        }

    }



}

