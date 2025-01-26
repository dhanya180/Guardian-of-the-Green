using UnityEngine;

public class minimapIcon : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public RectTransform minimap; // Reference to the minimap's RectTransform (for UI minimaps)

    void Update()
    {
        // Convert player's world position to minimap position
        Vector2 worldPos = new Vector2(player.position.x, player.position.z); // Assuming X and Z for a top-down map
        Vector2 minimapPos = WorldToMinimap(worldPos);

        // Set the icon's position on the minimap
        GetComponent<RectTransform>().anchoredPosition = minimapPos;

        transform.rotation = Quaternion.Euler(0, 0, -player.eulerAngles.y);

    }

    Vector2 WorldToMinimap(Vector2 worldPos)
    {
        // Adjust scale and offset based on the minimap's size and the world size
        float mapScale = 10f; // Adjust based on your world size
        return worldPos / mapScale;
    }
}
