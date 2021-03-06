using DigitalRuby.RainMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene4Controller : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public Transform path1;
    public Transform path2;
    public GameObject spawnZone1;
    public GameObject spawnZone2;
    public GameObject RainPrefab;

    List<GameObject> prefabList = new List<GameObject>();
    List<GameObject> prefabList2 = new List<GameObject>();
    public GameObject normalCar1;
    public GameObject normalCar2;
    public GameObject autocar1;
    public GameObject autocar2;
    public GameObject autocar3;
    int prefabIndex;
    // Start is called before the first frame update
    void Start()
    {
        prefabList.Add(autocar1);
        prefabList.Add(autocar2);
        prefabList.Add(autocar3);
        prefabList2.Add(normalCar1);
        prefabList2.Add(normalCar2);
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
            case "y":
                Debug.Log("toggle day/night");
                TriggerDayNight();
                break;
        }
    }

    private void SpawnCar(string carType)
    {
        SpawnZoneScript spawnZoneScript1 = spawnZone1.GetComponent<SpawnZoneScript>(); // retrieves the script instance of spawn zone 1
        SpawnZoneScript spawnZoneScript2 = spawnZone2.GetComponent<SpawnZoneScript>(); // retrieves the script instance of spawn zone 2

        GameObject targetCar = null;

        if (carType == "Normal")
        {
            prefabIndex = UnityEngine.Random.Range(0, 3);
            targetCar = prefabList2[prefabIndex];
        }
        else if (carType == "Auto")
        {
            prefabIndex = UnityEngine.Random.Range(0, 3);
            targetCar = prefabList[prefabIndex];
        }

        // spawn car in spawn zone 1 if there are no objects in the spawn zone
        if (!spawnZoneScript1.haveObjectInSpawnZone)
        {
            GameObject clonedCar = Instantiate(targetCar, new Vector3(75, 0, 3), Quaternion.Euler(0, -180, 0)); // Clone normal car at specified position and rotation.
            CarEngine clonedCarScript = clonedCar.gameObject.GetComponent<CarEngine>(); // retrieves the script instance from the clone
            clonedCarScript.path = path1; // assign path to cloned car
        }

        // spawn car in spawn zone 2 if there are no objects in the spawn zone
        if (!spawnZoneScript2.haveObjectInSpawnZone)
        {
            GameObject clonedCar = Instantiate(targetCar, new Vector3(-80, 0, 5), Quaternion.Euler(0, -180, 0)); // Clone normal car at specified position and rotation.
            CarEngine clonedCarScript = clonedCar.gameObject.GetComponent<CarEngine>(); // retrieves the script instance from the clone
            clonedCarScript.path = path2; // assign path to cloned car
        }
    }

    
    private void TriggerDayNight()
    {

        GameObject lightObject = GameObject.Find("Directional Light");
        var light = lightObject.GetComponent<Light>();
        var _materialOne = Resources.Load<Material>("night");
        var _materialTwo = Resources.Load<Material>("Skybox Light");


        //Material nightMat = new Material(Application.dataPath + "/Assets/script"+ "Night" +".mat");
        if (light.enabled)
        {
            RenderSettings.skybox = _materialOne;
            light.enabled = false;
            DynamicGI.UpdateEnvironment();

        }
        else
        {

            RenderSettings.skybox = _materialTwo;
            light.enabled = true;
            DynamicGI.UpdateEnvironment();
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
