using UnityEngine;
using System.Collections.Generic;


public class DayNightSystem : MonoBehaviour
{

    public Light directionalLight;
    public float dayDurationInSeconds = 24.0f;
    public int currentHour;
    float currentTimeOfDay=0.0f;
    public List<SkyboxTimeMapping>timeMapping;
    void Update()
    {
        currentTimeOfDay +=Time.deltaTime / dayDurationInSeconds;
        currentTimeOfDay%=1;
        currentHour = Mathf.FloorToInt(currentTimeOfDay*24);

        directionalLight.transform.rotation =Quaternion.Euler(new Vector3((currentTimeOfDay * 360) - 90,170,0));

        UpdateSkybox();
    }


    private void UpdateSkybox()
    {
        Material currentSkybox = null;
        foreach(SkyboxTimeMapping mapping in timeMapping)
        {
            if(currentHour == mapping.hour)
            {
                currentSkybox=mapping.skyboxMaterial;
                break;
            }
        }
        if(currentSkybox !=null)
        {
            RenderSettings.skybox = currentSkybox;
        }

    }
}



public class SkyboxTimeMapping
{
    public string phaseName;
    public int hour;
    public Material skyboxMaterial;
}