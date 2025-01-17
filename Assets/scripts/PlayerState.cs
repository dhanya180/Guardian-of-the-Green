using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static PlayerState Instance { get; private set; }
    public Transform PlayerBody;

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

    private void Start()
    {
        // Ensure playerBody is assigned
        if (PlayerBody == null)
        {
            Debug.LogError("Player body is not assigned in PlayerState!");
        }
    }

}
