// using UnityEngine;
// using UnityEngine.Events; // Add this line to access UnityEvent

// using TMPro;

// public class TimeManager : MonoBehaviour
// {
//     public static TimeManager Instance { get; private set; }

//     public UnityEvent OnDayPass = new UnityEvent();

//         private void Awake()
//     {
//         if (Instance != null && Instance != this)
//         {
//             Destroy(gameObject);
//         }
//         else
//         {
//             Instance = this;
//         }

        
//         DontDestroyOnLoad(gameObject);
//     }

//     public int dayInGame=1;
//     public TextMeshProUGUI dayUI;
//     private void Start()
//     {
//         dayUI.text =$"Day:{dayInGame}"; 
//     }
//     public void TriggerNextDay()
//     {
//         dayInGame+=1;
//         dayUI.text =$"Day:{dayInGame}";
//         OnDayPass.Invoke();
//     }

//     //  public void TriggerNextDay(int day)
//     // {
//     //     dayInGame=day;
//     //     dayInGame+=1;
//     //     dayUI.text =$"Day:{dayIngame}";
//     //     OnDayPass.Invoke();
//     // }


// }


// using UnityEngine;
// using UnityEngine.Events; // Add this line to access UnityEvent

// using TMPro;

// public class TimeManager : MonoBehaviour
// {
//     public static TimeManager Instance { get; private set; }

//     public UnityEvent OnDayPass = new UnityEvent();

//         private void Awake()
//     {
//         if (Instance != null && Instance != this)
//         {
//             Destroy(gameObject);
//         }
//         else
//         {
//             Instance = this;
            
//         }

        
//         DontDestroyOnLoad(gameObject);
//     }

//     public int dayInGame=1;
//     public TextMeshProUGUI dayUI;
//     private void Start()
//     {
//         dayUI.text =$"Day:{dayInGame}"; 
//     }
//     public void TriggerNextDay()
//     {
//         dayInGame+=1;
//         dayUI.text =$"Day:{dayInGame}";
//         OnDayPass.Invoke();
//     }

//     //  public void TriggerNextDay(int day)
//     // {
//     //     dayInGame=day;
//     //     dayInGame+=1;
//     //     dayUI.text =$"Day:{dayIngame}";
//     //     OnDayPass.Invoke();
//     // }


// }



// using UnityEngine;
// using UnityEngine.Events; // Add this line to access UnityEvent

// using TMPro;

// public class TimeManager : MonoBehaviour
// {
//     public static TimeManager Instance { get; private set; }
//     public int dayInGame=1;
//     public TextMeshProUGUI dayUI;
//     public UnityEvent OnDayPass = new UnityEvent();

//         private void Awake()
//     {
//         if (Instance != null && Instance != this)
//         {
//             Destroy(gameObject);
//         }
//         else
//         {
//             Instance = this;
//              DontDestroyOnLoad(gameObject);   
//         }
//     }
//     // private void Start()
//     // {
//     //     dayUI.text =$"Day:{dayInGame}"; 
//     // }

//      private void Start()
//     {
//         AssignDayUIText();
//         UpdateDayUI();
//     }

//      private void AssignDayUIText()
//     {
//         if (dayUI == null)
//         {
//             // Attempt to find the UI dynamically if it hasn't been assigned
//             dayUI = GameObject.Find("DayUIText")?.GetComponent<TextMeshProUGUI>();
//             if (dayUI == null)
//             {
//                 Debug.LogError("DayUIText not found! Make sure it exists in the scene.");
//             }
//         }
//     }

//      private void UpdateDayUI()
//     {
//         if (dayUI != null)
//         {
//             dayUI.text = $"Day: {dayInGame}";
//         }
//     }
//     // public void TriggerNextDay()
//     // {
//     //     dayInGame+=1;
//     //     dayUI.text =$"Day:{dayInGame}";
//     //     OnDayPass.Invoke();
//     // }


//     public void TriggerNextDay()
//     {
//         dayInGame++;
//         UpdateDayUI();
//     }
//     public void SetDayNumber(int savedDay)
//     {
//         dayInGame = savedDay;
//         UpdateDayUI();
//     }
// }


using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;


public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }
    public TextMeshProUGUI dayUI; // Assign dynamically or in Inspector
    public int dayInGame = 1;
    public UnityEvent OnDayPass = new UnityEvent();
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Dynamically find and assign the UI component after the scene loads
        AssignDayUIText();
        UpdateDayUI();
    }

    private void AssignDayUIText()
    {
        // Replace "DayUIText" with the actual name of your UI GameObject
        GameObject dayUITextObject = GameObject.Find("DayUI");
        if (dayUITextObject != null)
        {
            dayUI = dayUITextObject.GetComponent<TextMeshProUGUI>();
        }

        if (dayUI == null)
        {
            Debug.LogError("DayUIText not found or TextMeshProUGUI component is missing!");
        }
    }

    private void UpdateDayUI()
    {
        if (dayUI != null)
        {
            dayUI.text = $"Day: {dayInGame}";
        }
        else
        {
            Debug.LogWarning("dayUI is not assigned! Unable to update UI.");
        }
    }

    public void TriggerNextDay()
    {
        dayInGame++;
        UpdateDayUI();
    }

    public void SetDayNumber(int savedDay)
    {
        dayInGame = savedDay;
        UpdateDayUI();
    }
}
