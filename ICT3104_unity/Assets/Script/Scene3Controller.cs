using DigitalRuby.RainMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3Controller : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject normalCar;
    public GameObject autoCar;
    public GameObject trafficLight1;
    public GameObject trafficLight2;
    public GameObject spawnZone;
    public GameObject RainPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var pressedKey = Input.inputString;

        switch (pressedKey)
        {
            case "r":
                TriggerRain();
                break;
            case "-":
                SpawnCar("Normal");
                break;
            case "=":
                SpawnCar("Auto");
                break;
            case "t":
                TriggerTrafficLight();
                break;
			case "y":
				Debug.Log("toggle day/night");
				TriggerDayNight();
				break;
        }
    }

    private void SpawnCar(string carType)
    {
        SpawnZoneScript spawnZoneScript = spawnZone.GetComponent<SpawnZoneScript>(); // retrieves the script instance of spawn zone

        if (!spawnZoneScript.haveObjectInSpawnZone)
        {
            if (carType == "Normal")
            {
                // Clone normal car at specified position and rotation.
                Instantiate(normalCar, new Vector3(19, 0, -55), Quaternion.Euler(0, 270, 0));
            }
            else if (carType == "Auto")
            {
                // Clone auto car at specified position and rotation.
                Instantiate(autoCar, new Vector3(19, 0, -55), Quaternion.Euler(0, 270, 0));
            }
        }
    }

    private void TriggerTrafficLight()
    {
        TrafficLightController trafficLight1Script = trafficLight1.GetComponent<TrafficLightController>(); // retrieves the script instance of the trafficLight1
        TrafficLightController trafficLight2Script = trafficLight2.GetComponent<TrafficLightController>(); // retrieves the script instance of the trafficLight2

        if (!trafficLight1Script.isTrigger && !trafficLight2Script.isTrigger)
        {
            trafficLight1Script.isTrigger = true;
            trafficLight2Script.isTrigger = true;
        }
    }
	private void TriggerDayNight()
	{
		
		GameObject lightObject = GameObject.Find("Directional Light (1)");
		var light = lightObject.GetComponent<Light>();
		var _materialOne    = Resources.Load<Material>( "night" );
		var _materialTwo    = Resources.Load<Material>( "Skybox Light" );
	
		
		//Material nightMat = new Material(Application.dataPath + "/Assets/script"+ "Night" +".mat");
		if(light.enabled){
			RenderSettings.skybox = _materialOne;
			light.enabled =false;
			DynamicGI.UpdateEnvironment ();

		}
		else{
		
			RenderSettings.skybox = _materialTwo;
			light.enabled = true;
			DynamicGI.UpdateEnvironment ();
		}

	}

    private void TriggerRain()
    {
     
        RainScript rain = RainPrefab.GetComponent<RainScript>();

        if (rain.RainIntensity == 0)
        {
            rain.RainIntensity = 1;
        }
        else
        {
            rain.RainIntensity = 0;
        }

    }

}
