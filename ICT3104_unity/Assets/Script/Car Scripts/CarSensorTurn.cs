using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSensorTurn : MonoBehaviour
{
    public GameObject car;
    private CarEngine carEngineScript;

    // Start is called before the first frame update
    void Start()
    {
        carEngineScript = car.GetComponent<CarEngine>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When other objects is within the zone
    private void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.tag == "Car" || other.gameObject.tag == "Player") && carEngineScript.isTurning) // Checks if the object that entered the collider is a car
        {
            carEngineScript.isBraking = true;
            carEngineScript.stoppingForSomething = true;
        }
    }

    // When other objects leave the zone
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Car" || other.gameObject.tag == "Player") // Checks if the object that entered the collider is a car
        {
            carEngineScript.stoppingForSomething = false;

            // Prevent release of brakes when car is stopped at traffic light
            if (!carEngineScript.stoppingAtTrafficLight)
            {
                // release the brakes if they are not already released
                carEngineScript.isBraking = false;
            }
        }
    }
}
