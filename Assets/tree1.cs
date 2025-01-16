using UnityEngine;

public class tree1 : MonoBehaviour
{
    [Header("Prefab and Spawn Settings")]
    public GameObject tree_01; // Assign your prefab here
    public int numberOfObjects = 10; // Number of objects to spawn
    public Vector3 spawnAreaSize = new Vector3(10, 0, 10); // Size of spawn area

    [Header("Random Position Settings")]
    public Vector3 spawnAreaCenter = Vector3.zero; // Center of the spawn area

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            // Generate a random position within the specified area
            Vector3 randomPosition = new Vector3(
                Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2, spawnAreaCenter.x + spawnAreaSize.x / 2),
                Random.Range(spawnAreaCenter.y - spawnAreaSize.y / 2, spawnAreaCenter.y + spawnAreaSize.y / 2),
                Random.Range(spawnAreaCenter.z - spawnAreaSize.z / 2, spawnAreaCenter.z + spawnAreaSize.z / 2)
            );

            // Instantiate the prefab at the random position
            Instantiate(tree_01, randomPosition, Quaternion.identity);
        }
    }
}

