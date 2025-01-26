using UnityEngine;
using System.Collections;

public class FireBehavior : MonoBehaviour
{
    public GameManager gameManager; // Reference to the GameManager script
    public GameObject smokePrefab; // Smoke prefab to instantiate
    public float smokeDuration = 5f; // Duration for the smoke to last
    public float smokeRange = 1.0f; // Initial range of the smoke (controls the size of the smoke)
    public float fireExtinguishTime = 1f; // Time to decrease fire intensity
    private ParticleSystem fireParticleSystem; // Reference to the fire particle system
    private bool isExtinguishing = false; // Flag to prevent multiple extinguishing processes
    private Renderer treeRenderer; // Renderer for the tree to change leaf colors
    private Material treeMaterial; // Material of the tree
    private Color originalLeafColor; // Original leaf color
    private GameObject smokeInstance; // Smoke instance
    public bool isExtinguished { get; private set; } // Whether the fire is extinguished

    void Start()
    {
        // Find the ParticleSystem on the fire object or its children
        fireParticleSystem = GetComponentInChildren<ParticleSystem>();

        if (fireParticleSystem == null)
        {
            Debug.LogError($"No ParticleSystem found on {gameObject.name} or its children.");
        }

        // Get the tree associated with the fire to change leaf color
        treeRenderer = GetComponentInParent<Renderer>();
        if (treeRenderer != null)
        {
            treeMaterial = treeRenderer.material;
            originalLeafColor = treeMaterial.color;
        }

        // Automatically find and assign the GameManager
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene. Please add a GameManager!");
        }
    }

    private void OnMouseDown()
    {
        if (!isExtinguishing)
        {
            isExtinguishing = true;
            Debug.Log("Fire clicked. Starting extinguishing process.");
            StartCoroutine(ExtinguishFireAndChangeLeaves());
        }
    }

    private IEnumerator ExtinguishFireAndChangeLeaves()
    {
        // Gradually reduce the fire's emission rate
        if (fireParticleSystem != null)
        {
            var emission = fireParticleSystem.emission;
            float elapsedTime = 0f;

            while (elapsedTime < fireExtinguishTime)
            {
                emission.rateOverTime = Mathf.Lerp(50f, 0f, elapsedTime / fireExtinguishTime);

                // Gradually change the tree's leaf color to black-green
                float t = elapsedTime / fireExtinguishTime;
                Color newLeafColor = Color.Lerp(originalLeafColor, Color.black, t);
                if (treeMaterial != null)
                {
                    treeMaterial.color = newLeafColor;
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            fireParticleSystem.Stop();
            isExtinguished = true;
            Debug.Log("Fire stopped.");
        }

        // Instantiating smoke immediately
        if (smokePrefab != null)
        {
            smokeInstance = Instantiate(smokePrefab, transform.position, Quaternion.identity);
            smokeInstance.transform.localScale = new Vector3(smokeRange, smokeRange, smokeRange);
            Debug.Log("Smoke instantiated.");
        }
        else
        {
            Debug.LogWarning("Smoke prefab not assigned.");
        }

        // Notify GameManager that this fire has been extinguished
        if (gameManager != null)
        {
            gameManager.FireExtinguished();
        }

        // Gradually decrease smoke size over time
        float smokeElapsedTime = 0f;
        while (smokeElapsedTime < smokeDuration)
        {
            float scale = Mathf.Lerp(smokeRange, 0f, smokeElapsedTime / smokeDuration);
            if (smokeInstance != null)
            {
                smokeInstance.transform.localScale = new Vector3(scale, scale, scale);
            }
            smokeElapsedTime += Time.deltaTime;
            yield return null;
        }

        if (smokeInstance != null)
        {
            Destroy(smokeInstance);
        }

        // Destroy fire object after smoke effect
        Destroy(gameObject);
        Debug.Log("Fire destroyed.");
    }
}
