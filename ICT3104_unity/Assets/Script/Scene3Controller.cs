using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3Controller : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject normalCar;
    public GameObject trafficLight1;
    public GameObject trafficLight2;
    public GameObject spawnZone;

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
            case "-":
                SpawnNormalCar();
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

    private void SpawnNormalCar()
    {
        SpawnZoneScript spawnZoneScript = spawnZone.GetComponent<SpawnZoneScript>(); // retrieves the script instance of spawn zone

        if (!spawnZoneScript.haveObjectInSpawnZone)
        {
            // Clone normal car at specified position and rotation.
            Instantiate(normalCar, new Vector3(19, 0, -55), Quaternion.Euler(0, 270, 0));
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
		var _materialOne    = Resources.Load<Material>( "Assets/script/night.mat" );
		var _materialTwo    = Resources.Load<Material>( "Assets/ExampleAssets/Materials/Skybox Light.mat" );
		
		//Material nightMat = new Material(Application.dataPath + "/Assets/script"+ "Night" +".mat");
		if(light.enabled){
		//	RenderSettings.skybox = _materialTwo;
			light.enabled =false;
		}
		else{
		
			Debug.Log("Here");
		//	RenderSettings.skybox = _materialOne;
			light.enabled = true;
		}

	}
}
