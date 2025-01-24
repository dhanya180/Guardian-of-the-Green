using UnityEngine;
using UnityEngine.UI; // Required for Button
using TMPro; // Required for TextMeshProUGUI
using System; // Required for DateTime
using UnityEngine.EventSystems;
public class LoadSlot : MonoBehaviour
{
    private Button button;
    private TextMeshProUGUI buttonText;
    public int slotNumber;
    private void Awake()
    {
        button = GetComponent<Button>();
        buttonText = transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(SaveManager.Instance.IsSlotEmpty(slotNumber))
        {
            buttonText.text= "";
        }
        else{
            buttonText.text=PlayerPrefs.GetString("Slot"+slotNumber+"Description");
        }
    }

    private void Start()
    {
        button.onClick.AddListener(()=>
        {
            if( SaveManager.Instance.IsSlotEmpty(slotNumber)== false){
                SaveManager.Instance.StartLoadedGame(slotNumber);
                SaveManager.Instance.DeselectButton();
        }
        else{

        }
        }
        );
    }
}
