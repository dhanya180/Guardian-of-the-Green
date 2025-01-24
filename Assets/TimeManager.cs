using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }
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

        
        DontDestroyOnLoad(gameObject);
    }

    public int dayInGame=1;
    public TextMeshProUGUI dayUI;
    private void Start()
    {
        dayUI.text =$"Day:{dayInGame}"; 
    }
    public void TriggerNextDay()
    {
        dayInGame+=1;
        dayUI.text =$"Day:{dayInGame}";
    }


}
