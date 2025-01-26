using UnityEngine;

public class minimapScirpt : MonoBehaviour
{
    public Transform player;

    void LateUpdate ()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90F,player.eulerAngles.y,0f);
    }
}
