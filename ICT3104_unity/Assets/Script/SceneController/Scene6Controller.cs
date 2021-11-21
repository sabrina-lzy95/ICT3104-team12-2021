using DigitalRuby.RainMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Globalization;

public class Scene6Controller : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public Transform path1;
    public Transform path2;
    public GameObject trafficLight1;
    public GameObject trafficLight2; 
    public GameObject trafficLight3;
    public GameObject trafficLight4;
    public GameObject spawnZone1;
    public GameObject RainPrefab;

    List<GameObject> prefabList = new List<GameObject>();
    public GameObject normalCar;
    public GameObject autocar1;
    public GameObject autocar2;
    public GameObject autocar3;
	StreamWriter writer;
	DateTime localDate;
	
    int prefabIndex;

    // Start is called before the first frame update
    void Start()
    {
		localDate = DateTime.Now;
		string path = "Assets/Resources/test.log";
		writer = new StreamWriter(path, true);    
		writer.WriteLine(localDate.ToString() + ","+ "Loaded Scene 6" );
        prefabList.Add(autocar1);
        prefabList.Add(autocar2);
        prefabList.Add(autocar3);
		StartCoroutine("DoCheck");
		    }

    // Update is called once per frame
	 IEnumerator DoCheck() {
     for(;;) {
         // execute block of code here
		localDate = DateTime.Now;
		var playerObj  = GameObject.FindGameObjectWithTag("Player");
		Console.WriteLine(playerObj);
		writer.WriteLine(localDate.ToString() + ","+"User Location x: "+ playerObj.transform.position.x+" y: "+playerObj.transform.position.z);

    yield return new WaitForSeconds(10.0f);
     }
 }
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

	void OnDestroy(){
		writer.Close();
	}
    private void SpawnCar(string carType)
    {
		localDate = DateTime.Now;
        SpawnZoneScript spawnZoneScript1 = spawnZone1.GetComponent<SpawnZoneScript>(); // retrieves the script instance of spawn zone 1
        //SpawnZoneScript spawnZoneScript2 = spawnZone2.GetComponent<SpawnZoneScript>(); // retrieves the script instance of spawn zone 2

        GameObject targetCar = null;

        if (carType == "Normal")
        {
			writer.WriteLine(localDate.ToString() + ",Spawned Normal Car!");
            targetCar = normalCar;
        }
        else if (carType == "Auto")
        {
			writer.WriteLine(localDate.ToString() + ",Spawned Auto Car!");
            prefabIndex = UnityEngine.Random.Range(0, 3);
            targetCar = prefabList[prefabIndex];
        }

        // spawn car in spawn zone 1 if there are no objects in the spawn zone
        if (!spawnZoneScript1.haveObjectInSpawnZone)
        {
            GameObject clonedCar = Instantiate(targetCar, new Vector3(83, 1, 21), Quaternion.Euler(0, -180, 0)); // Clone normal car at specified position and rotation.
            CarEngine clonedCarScript = clonedCar.gameObject.GetComponent<CarEngine>(); // retrieves the script instance from the clone
            clonedCarScript.path = path1; // assign path to cloned car
        }

        // spawn car in spawn zone 2 if there are no objects in the spawn zone
       /* if (!spawnZoneScript2.haveObjectInSpawnZone)
        {
            GameObject clonedCar = Instantiate(targetCar, new Vector3(-13, 1, -92), Quaternion.Euler(0, -270, 0)); // Clone normal car at specified position and rotation.
            CarEngine clonedCarScript = clonedCar.gameObject.GetComponent<CarEngine>(); // retrieves the script instance from the clone
            clonedCarScript.path = path2; // assign path to cloned car
        }*/
    }

    private void TriggerTrafficLight()
    {
		localDate = DateTime.Now;
		writer.WriteLine(localDate.ToString() + ",Triggered Traffic Light!");

        TrafficLightController trafficLight1Script = trafficLight1.GetComponent<TrafficLightController>(); // retrieves the script instance of the trafficLight1
        TrafficLightController trafficLight2Script = trafficLight2.GetComponent<TrafficLightController>(); // retrieves the script instance of the trafficLight2
        TrafficLightController trafficLight3Script = trafficLight3.GetComponent<TrafficLightController>(); // retrieves the script instance of the trafficLight3
        TrafficLightController trafficLight4Script = trafficLight4.GetComponent<TrafficLightController>(); // retrieves the script instance of the trafficLight4

        if (!trafficLight1Script.isTrigger && !trafficLight2Script.isTrigger && !trafficLight3Script.isTrigger && !trafficLight4Script.isTrigger)
        {
            trafficLight1Script.isTrigger = true;
            trafficLight2Script.isTrigger = true;
            trafficLight3Script.isTrigger = true;
            trafficLight4Script.isTrigger = true;
        }
    }
    private void TriggerDayNight()
    {

		localDate = DateTime.Now;
        GameObject lightObject = GameObject.Find("Directional Light");
        var light = lightObject.GetComponent<Light>();
        var _materialOne = Resources.Load<Material>("night");
        var _materialTwo = Resources.Load<Material>("Skybox Light");


        //Material nightMat = new Material(Application.dataPath + "/Assets/script"+ "Night" +".mat");
        if (light.enabled)
        {
			writer.WriteLine(localDate.ToString() + ",Night Triggered");
            RenderSettings.skybox = _materialOne;
            light.enabled = false;
            DynamicGI.UpdateEnvironment();

        }
        else
        {

			writer.WriteLine(localDate.ToString() + ",Day Triggered");
            RenderSettings.skybox = _materialTwo;
            light.enabled = true;
            DynamicGI.UpdateEnvironment();
        }

    }

    private void TriggerRain()
    {

		localDate = DateTime.Now;
        RainScript rain = RainPrefab.GetComponent<RainScript>();

        if (rain.RainIntensity == 0)
        {
			writer.WriteLine(localDate.ToString() + ",Rain Started");
            rain.RainIntensity = 1;
        }
        else
        {
			writer.WriteLine(localDate.ToString() + ",Rain Ended");
            rain.RainIntensity = 0;
        }

    }
}
