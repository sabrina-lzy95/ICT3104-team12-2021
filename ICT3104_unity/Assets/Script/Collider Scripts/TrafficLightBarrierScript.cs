using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightBarrierScript : MonoBehaviour
{
    public GameObject trafficLight;

    private TrafficLightController trafficLightScript;
    private Collider collider;
    private GameObject car;
    //private CarEngine carScript;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        trafficLightScript = trafficLight.GetComponent<TrafficLightController>(); // retrieves the script instance of the traffic light
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (trafficLightScript.LightState.ToString() == "yellow2")
        {
            if (trafficLightScript.LightCount < 1)
            {
                collider.enabled = true;
            }
        }
        else if (trafficLightScript.LightState.ToString() == "green")
        {
            collider.enabled = false;
            // tell the car to release brakes 
            if (car != null)
            {
                car.GetComponent<CarEngine>().isBraking = false;
                car.GetComponent<CarEngine>().stoppingAtCrossing = false;
                car = null;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            // Other is the near sensor of the car, we need access the car itself to check the tag
            // save the car object to the car variable so we can use it to control the car
            car = other.gameObject.transform.parent.gameObject.transform.parent.gameObject;
        }
    }
}
