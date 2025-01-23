
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic; // Corrected this line
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
   // public Button LoadGameBTN;
    

    public void NewGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    } 
}
