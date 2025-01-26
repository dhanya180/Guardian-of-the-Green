using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RandomTreeMultipleFires : MonoBehaviour
{
    public GameObject[] trees; // Array to hold the trees
    public GameObject firePrefab; // Fire effect prefab
    public int fireCount = 2; // Number of fires to spawn
    public float fireHeight = 7f; // Height of the fire effect above the ground
    public GameObject smokePrefab; // Smoke prefab to instantiate
    public float smokeDuration = 5f; // Duration for the smoke to last
    public float delayBetweenFires = 1f; // Delay between spawning each fire

    void Start()
    {
        StartCoroutine(SpawnFiresWithDelay());
    }

    IEnumerator SpawnFiresWithDelay()
    {
        // Shuffle the tree indices
        List<int> indices = new List<int>();
        for (int i = 0; i < trees.Length; i++) indices.Add(i);
        indices = indices.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < Mathf.Min(fireCount, indices.Count); i++)
        {
            GameObject selectedTree = trees[indices[i]];

            // Instantiate the fire effect at the selected tree's position with an offset
            if (firePrefab != null && selectedTree != null)
            {
                Vector3 firePosition = selectedTree.transform.position;
                firePosition.y += fireHeight; // Adjust the height

                // Instantiate the fire
                GameObject fireInstance = Instantiate(firePrefab, firePosition, Quaternion.identity);

                // Add FireBehavior dynamically
                FireBehavior fireBehavior = fireInstance.GetComponent<FireBehavior>();
                fireBehavior.smokePrefab = smokePrefab;
                fireBehavior.smokeDuration = smokeDuration;
            }
            else
            {
                Debug.LogError("FirePrefab or Trees array is not set!");
            }

            // Wait for the specified delay before spawning the next fire
            yield return new WaitForSeconds(delayBetweenFires);
        }
    }

    void SpawnFires()
    {
        // Shuffle the tree indices
        List<int> indices = new List<int>();
        for (int i = 0; i < trees.Length; i++) indices.Add(i);
        indices = indices.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < Mathf.Min(fireCount, indices.Count); i++)
        {
            GameObject selectedTree = trees[indices[i]];

            // Instantiate the fire effect at the selected tree's position with an offset
            if (firePrefab != null && selectedTree != null)
            {
                Vector3 firePosition = selectedTree.transform.position;
                firePosition.y += fireHeight; // Adjust the height

                // Instantiate the fire
                GameObject fireInstance = Instantiate(firePrefab, firePosition, Quaternion.identity);

                // Add FireBehavior dynamically
                FireBehavior fireBehavior = fireInstance.GetComponent<FireBehavior>();
                fireBehavior.smokePrefab = smokePrefab;
                fireBehavior.smokeDuration = smokeDuration;
            }
            else
            {
                Debug.LogError("FirePrefab or Trees array is not set!");
            }
        }
    }
}