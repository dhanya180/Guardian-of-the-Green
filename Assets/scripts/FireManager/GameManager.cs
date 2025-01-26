using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Slider oxygenSlider; // UI slider to display oxygen level
    public float oxygenLevel = 100f; // Starting oxygen level
    public float baseOxygenDecreaseRate = 1f; // Base rate of oxygen decrease
    public int totalFires = 0; // Total number of active fires
    private bool gameOver = false; // Game over flag

    void Start()
    {
        // Initialize the slider's max value and starting value
        if (oxygenSlider != null)
        {
            oxygenSlider.maxValue = oxygenLevel;
            oxygenSlider.value = oxygenLevel;
        }
    }

    void Update()
    {
        if (gameOver) return;

        if (totalFires > 0)
        {
            // Decrease oxygen level based on the number of fires
            oxygenLevel -= baseOxygenDecreaseRate * totalFires * Time.deltaTime;

            // Update the slider UI
            if (oxygenSlider != null)
            {
                oxygenSlider.value = oxygenLevel;
            }

            // Check if oxygen level has reached 0
            if (oxygenLevel <= 0)
            {
                GameOver();
            }
        }
    }

    public void FireExtinguished()
    {
        if (totalFires > 0)
        {
            totalFires--; // Decrease the total number of active fires
            Debug.Log($"Fire extinguished. Remaining fires: {totalFires}");
        }

        // Stop oxygen depletion if all fires are extinguished
        if (totalFires == 0)
        {
            Debug.Log("All fires extinguished. Oxygen depletion stopped.");
        }
    }

    private void GameOver()
    {
        gameOver = true;
        Debug.Log("Game Over! Oxygen depleted.");
        // Implement additional game-over logic here (e.g., display game-over UI)
    }
}
