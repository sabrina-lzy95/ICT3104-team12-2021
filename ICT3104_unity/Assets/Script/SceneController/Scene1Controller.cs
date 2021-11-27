using DigitalRuby.RainMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Scene1Controller : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public Transform path1;
    public GameObject trafficLight1;
    public GameObject trafficLight2;
    public GameObject spawnZone1;
    
    public GameObject RainPrefab;

    List<GameObject> prefabList = new List<GameObject>();
    public GameObject normalCar;
    public GameObject autocar1;
    public GameObject autocar2;
    public GameObject autocar3;
    int prefabIndex;
	StreamWriter writer;
	DateTime localDate;

    // Start is called before the first frame update
    void Start()
    {
        prefabList.Add(autocar1);
        prefabList.Add(autocar2);
        prefabList.Add(autocar3);

		localDate = DateTime.Now;
		string path = "Assets/Resources/Log.csv";
		writer = new StreamWriter(path, true);    
		writer.WriteLine(localDate.ToString() + ","+ "Loaded Scene 1" );
		PlayerPrefs.SetString ("log",PlayerPrefs.GetString("log")+"\n"+localDate.ToString() + ","+ "Loaded Scene 1" );
		StartCoroutine("LogUserLocation");
    }

	 IEnumerator LogUserLocation() {
		 for(;;) {
			 // execute block of code here
			localDate = DateTime.Now;
			var playerObj  = GameObject.FindGameObjectWithTag("Player");
			Console.WriteLine(playerObj);
			writer.WriteLine(localDate.ToString() + ","+"User Location x: "+ playerObj.transform.position.x+" y: "+playerObj.transform.position.z);
			PlayerPrefs.SetString ("log",PlayerPrefs.GetString("log")+"\n"+localDate.ToString() + ","+"User Location x: "+ playerObj.transform.position.x.ToString("0.00")+" y: "+playerObj.transform.position.z.ToString("0.00"));
			yield return new WaitForSeconds(2.5f);
		}
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

	void OnDestroy(){
		localDate = DateTime.Now;
		writer.WriteLine(localDate.ToString() + ",Leaving Scene 1!");
		writer.WriteLine( "####################,###################");
		writer.WriteLine( "####################,###################");

		PlayerPrefs.SetString ("log",PlayerPrefs.GetString("log")+"\n"+localDate.ToString() + ","+ "Leaving Scene 1!" );
		PlayerPrefs.SetString ("log",PlayerPrefs.GetString("log")+"\n"+localDate.ToString() + ","+ "####################,###################" );
		PlayerPrefs.SetString ("log",PlayerPrefs.GetString("log")+"\n"+localDate.ToString() + ","+ "####################,###################" );
		writer.Close();
	}
    private void SpawnCar(string carType)
    {
		localDate = DateTime.Now;
        SpawnZoneScript spawnZoneScript1 = spawnZone1.GetComponent<SpawnZoneScript>(); // retrieves the script instance of spawn zone 1
        

        GameObject targetCar = null;

        if (carType == "Normal")
        {
			PlayerPrefs.SetString ("log",PlayerPrefs.GetString("log")+"\n"+localDate.ToString() + ","+ "Spawned Normal Car!" );
			writer.WriteLine(localDate.ToString() + ",Spawned Normal Car!");
            targetCar = normalCar;
        }
        else if (carType == "Auto")
        {
			PlayerPrefs.SetString ("log",PlayerPrefs.GetString("log")+"\n"+localDate.ToString() + ","+ "Spawned Auto Car!" );
			writer.WriteLine(localDate.ToString() + ",Spawned Auto Car!");
            prefabIndex = UnityEngine.Random.Range(0, 3);
            targetCar = prefabList[prefabIndex];
        }

        // spawn car in spawn zone 1 if there are no objects in the spawn zone
        if (!spawnZoneScript1.haveObjectInSpawnZone)
        {
            GameObject clonedCar = Instantiate(targetCar, new Vector3(4, 1, 113), Quaternion.Euler(0, -90, 0)); // Clone normal car at specified position and rotation.
            CarEngine clonedCarScript = clonedCar.gameObject.GetComponent<CarEngine>(); // retrieves the script instance from the clone
            clonedCarScript.path = path1; // assign path to cloned car
        }

    }

    private void TriggerTrafficLight()
    {
		localDate = DateTime.Now;
		writer.WriteLine(localDate.ToString() + ",Triggered Traffic Light!");
		writer.WriteLine(localDate.ToString() + ",Triggered Traffic Light!");
		PlayerPrefs.SetString ("log",PlayerPrefs.GetString("log")+"\n"+localDate.ToString() + ","+ "Triggered Traffic Light!" );
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

		localDate = DateTime.Now;
			writer.WriteLine(localDate.ToString() + ",Night Triggered");
        GameObject lightObject = GameObject.Find("Directional Light");
        var light = lightObject.GetComponent<Light>();
        var _materialOne = Resources.Load<Material>("night");
        var _materialTwo = Resources.Load<Material>("Skybox Light");


        //Material nightMat = new Material(Application.dataPath + "/Assets/script"+ "Night" +".mat");
        if (light.enabled)
        {
			PlayerPrefs.SetString ("log",PlayerPrefs.GetString("log")+"\n"+localDate.ToString() + ","+ "Day Triggered" );
			writer.WriteLine(localDate.ToString() + ",Day Triggered");
            RenderSettings.skybox = _materialOne;
            light.enabled = false;
            DynamicGI.UpdateEnvironment();

        }
        else
        {

			PlayerPrefs.SetString ("log",PlayerPrefs.GetString("log")+"\n"+localDate.ToString() + ","+ "Night Triggered" );
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
			PlayerPrefs.SetString ("log",PlayerPrefs.GetString("log")+"\n"+localDate.ToString() + ","+ "Rain Started" );
			writer.WriteLine(localDate.ToString() + ",Rain Started");
            rain.RainIntensity = 1;
        }
        else
        {
			PlayerPrefs.SetString ("log",PlayerPrefs.GetString("log")+"\n"+localDate.ToString() + ","+ "Rain Ended" );
			writer.WriteLine(localDate.ToString() + ",Rain Ended");
			writer.WriteLine(localDate.ToString() + ",Rain Ended");
            rain.RainIntensity = 0;
        }

    }

}
