using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightZoneScript : MonoBehaviour
{
    public GameObject trafficLight;

    private TrafficLightController trafficLightScript;
    private CarEngine carScript;

    // Start is called before the first frame update
    void Start()
    {
        trafficLightScript = trafficLight.GetComponent<TrafficLightController>(); // retrieves the script instance of the traffic light
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When other objects enter the zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car") // Checks if the object that entered the collider is a car
        {
            CarEngine carScript = other.gameObject.transform.parent.gameObject.GetComponent<CarEngine>(); // retrieves the script instance from the car to control the car

            if (trafficLightScript.LightState.ToString() == "yellow2" && trafficLightScript.LightCount < 1)
            {
                carScript.isBraking = true;
                carScript.stoppingAtTrafficLight = true;
            }
            else if (trafficLightScript.LightState.ToString() == "red")
            {
                carScript.isBraking = true;
                carScript.stoppingAtTrafficLight = true;
            }
        }
    }

    // When other objects are within the zone
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Car") // Checks if the object that entered the collider is a car
        {
            CarEngine carScript = other.gameObject.transform.parent.gameObject.GetComponent<CarEngine>(); // retrieves the script instance from the car to control the car

            if (trafficLightScript.LightState.ToString() == "green" && !carScript.stoppingForSomething)
            {
                carScript.isBraking = false;
                carScript.stoppingAtTrafficLight = false;
            }
        }
    }
}
