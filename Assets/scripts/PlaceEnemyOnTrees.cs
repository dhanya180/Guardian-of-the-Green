using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlaceEnemyOnTrees : MonoBehaviour
{
    public List<GameObject> trees; // Drag and drop tree objects in the Inspector
    public GameObject enemyPrefab; // Drag your enemy prefab here
    public int numberOfRandomTrees = 3; // Number of random trees to select
    public float distanceFromTree = 2.0f; // Fixed distance from the tree

    void Start()
    {
        if (trees.Count < numberOfRandomTrees)
        {
            Debug.LogError("Not enough trees assigned for the desired number of random selections!");
            return;
        }

        // List to store selected trees
        List<GameObject> selectedTrees = new List<GameObject>();

        while (selectedTrees.Count < numberOfRandomTrees)
        {
            // Pick a random tree
            GameObject randomTree = trees[Random.Range(0, trees.Count)];

            // Ensure it hasn't been selected already
            if (!selectedTrees.Contains(randomTree))
            {
                selectedTrees.Add(randomTree);
            }
        }

        // Place enemies near the selected trees
        foreach (GameObject tree in selectedTrees)
        {
            // Calculate the spawn position at a fixed distance on the ground
            Vector3 direction = Random.onUnitSphere; // Get a random direction
            direction.y = 0; // Flatten the direction to ensure it's on the ground
            direction.Normalize(); // Ensure the vector has a length of 1

            Vector3 spawnPosition = tree.transform.position + direction * distanceFromTree;
            spawnPosition.y = tree.transform.position.y; // Keep on the ground level

            // Instantiate enemy at the calculated position
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // Make the enemy face the tree
            enemy.transform.LookAt(tree.transform);

            // Tilt the enemy slightly downwards to face the ground near the tree
            enemy.transform.Rotate(15, 0, 0); // Adjust the angle to look down

            // Debug log to confirm health setup


        }

        Debug.Log($"Enemies placed near {numberOfRandomTrees} random trees at a fixed distance.");
    }
}