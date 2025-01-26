using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Required for scene management

public class GameManager : MonoBehaviour
{
    public Slider oxygenSlider; // UI slider to display oxygen level
    public float oxygenLevel = 100f; // Starting oxygen level
    public float baseOxygenDecreaseRate = 1f; // Base rate of oxygen decrease
    public int totalFires = 0; // Total number of active fires
    private bool gameOver = false; // Game over flag

    public GameObject gameOverUI; // Reference to Game Over UI panel
    public float delayBeforeMainMenu = 3f; // Delay before loading the MainMenu scene

    void Start()
    {
         Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
        // Initialize the slider's max value and starting value
        if (oxygenSlider != null)
        {
            oxygenSlider.maxValue = oxygenLevel;
            oxygenSlider.value = oxygenLevel;
        }

        // Ensure Game Over UI is initially disabled
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
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
         if (Input.GetKeyDown(KeyCode.Escape)) // Escape key toggles cursor
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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

        // Display Game Over UI
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        // Delay and then load the MainMenu scene
        Invoke(nameof(LoadMainMenu), delayBeforeMainMenu);
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
