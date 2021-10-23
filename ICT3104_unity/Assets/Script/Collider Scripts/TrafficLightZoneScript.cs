using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightZoneScript : MonoBehaviour
{
    public GameObject trafficLight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When other objects enter the zone
    private void OnTriggerEnter(Collider other)
    {
        TrafficLightController trafficLightScript = trafficLight.GetComponent<TrafficLightController>(); // retrieves the script instance of the traffic light

        if (other.gameObject.tag == "Car") // Checks if the object that entered the collider is a car
        {
            if (trafficLightScript.LightState.ToString() == "yellow2" && trafficLightScript.LightCount < 1)
            {
                CarEngine carScript = other.gameObject.transform.parent.gameObject.GetComponent<CarEngine>(); // retrieves the script instance from the car to control the car
                carScript.isBraking = true;
                Debug.Log(trafficLightScript.LightState);
            }
            else if (trafficLightScript.LightState.ToString() == "red")
            {
                CarEngine carScript = other.gameObject.transform.parent.gameObject.GetComponent<CarEngine>(); // retrieves the script instance from the car to control the car
                carScript.isBraking = true;
                Debug.Log(trafficLightScript.LightState);
            }
        }
    }

    // When other objects are within the zone
    private void OnTriggerStay(Collider other)
    {
        TrafficLightController trafficLightScript = trafficLight.GetComponent<TrafficLightController>(); // retrieves the script instance of the traffic light

        if (other.gameObject.tag == "Car") // Checks if the object that entered the collider is a car
        {
            if (trafficLightScript.LightState.ToString() == "green")
            {
                CarEngine carScript = other.gameObject.transform.parent.gameObject.GetComponent<CarEngine>(); // retrieves the script instance from the car to control the car
                carScript.isBraking = false;
                Debug.Log(trafficLightScript.LightState);
            }
        }
    }
}
