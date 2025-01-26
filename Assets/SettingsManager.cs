using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static SaveManager;
using System.Collections; // Add this to use IEnumerator and coroutines


public class SettingsManager : MonoBehaviour
{

    public static SettingsManager Instance { get; private set; }
    public Button backBTN;

    public Slider masterSlider;
    public GameObject masterValue;
    public Slider musicSlider;
    public GameObject musicValue;
    public Slider effectsSlider;
    public GameObject effectsValue;

    private void Start()
    {

        if (backBTN == null)
    {
        Debug.LogError("Back button is not assigned in the Inspector.");
        return;
    }
        backBTN.onClick.AddListener(() =>
        {
             if (SaveManager.Instance == null)
        {
            Debug.LogError("SaveManager instance is null. Cannot save volume settings.");
            return;
        }
            SaveManager.Instance.SaveVolumeSettings(musicSlider.value,effectsSlider.value,masterSlider.value);
            
        });

        StartCoroutine(LoadAndApplySettings());
    }

    private IEnumerator LoadAndApplySettings()
    {
        LoadAndSetVolume();

        
        yield return new WaitForSeconds(0.1f);
    }

    private void LoadAndSetVolume()
    {

        if (SaveManager.Instance == null)
    {
        Debug.LogError("SaveManager instance is null. Ensure it is properly initialized.");
        return;
    }
        VolumeSettings volumeSettings = SaveManager.Instance.LoadVolumeSettings();
         if (volumeSettings == null)
   {
    Debug.LogError("Failed to load volume settings. Ensure the data exists in SaveManager.");
    return;
   }
   if (masterSlider == null || musicSlider == null || effectsSlider == null)
{
    Debug.LogError("One or more sliders are not assigned in the Inspector.");
    return;
}
        masterSlider.value = volumeSettings.master; 
        musicSlider.value = volumeSettings.music;   
        effectsSlider.value = volumeSettings.effects;

        print("Volume Settings are Loaded");
    }


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

    private void Update()
    {
        masterValue.GetComponent<TextMeshProUGUI>().text = "" + (masterSlider.value) + "";
        musicValue.GetComponent<TextMeshProUGUI>().text = "" + (musicSlider.value) + "";
        effectsValue.GetComponent<TextMeshProUGUI>().text = "" + (effectsSlider.value) + "";
    }
}