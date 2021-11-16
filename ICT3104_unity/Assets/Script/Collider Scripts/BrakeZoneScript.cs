using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeZoneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // When other objects is within the zone
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Normal Car" || other.gameObject.tag == "Auto Car") // Checks if the object that entered the collider is a car
        {
            CarEngine carScript = other.gameObject.transform.parent.gameObject.GetComponent<CarEngine>(); // retrieves the script instance from the car to control the car

            // Apply brakes if the car's speed is too high
            if (carScript.currentSpeed > 6)
            {
                carScript.isBraking = true;
            }
            else if (!carScript.stoppingForSomething)
            {
                carScript.isBraking = false;
            }
        }
    }

    // When other objects leave the zone
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Normal Car" || other.gameObject.tag == "Auto Car") // Checks if the object that entered the collider is a car
        {
            CarEngine carScript = other.gameObject.transform.parent.gameObject.GetComponent<CarEngine>(); // retrieves the script instance from the car to control the car
            // release the brakes if they are not already released
            carScript.isBraking = false;
        }
    }
}
