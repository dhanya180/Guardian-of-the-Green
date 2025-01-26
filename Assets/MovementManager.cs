using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public static MovementManager Instance { get; private set; }

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

    public bool canMove = true;
    public bool canLookAround=true;
    public void EnableMovement(bool trigger)
    {
        canMove = trigger;
    }

    public void EnableLook(bool trigger)
    {
        canLookAround = trigger;
    }
}
