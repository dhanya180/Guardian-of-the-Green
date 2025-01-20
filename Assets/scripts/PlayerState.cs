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
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentHealth -= 10;
        }

    }



}

