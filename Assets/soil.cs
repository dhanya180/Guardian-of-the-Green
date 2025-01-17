using UnityEngine;

public class Soil : MonoBehaviour
{
    public bool isEmpty = true;
    public bool playerInRange;

    private void Update()
    {
        float distance=Vector3.Distance(PlayerState.Instance.PlayerBody.transform.position,transform.position);
        if (distance < 10f)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange=false;
        }
    }
}
