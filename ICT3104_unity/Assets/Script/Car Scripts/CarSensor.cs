using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSensor : MonoBehaviour
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
        if (other.gameObject.tag == "Normal Car" || other.gameObject.tag == "Auto Car" || other.gameObject.tag == "Player")
        {
            carEngineScript.isBraking = true;
            carEngineScript.stoppingForSomething = true;
        }
        else if (other.gameObject.tag == "Crossing Zone")
        {
            carEngineScript.isBraking = true;
            carEngineScript.stoppingAtCrossing = true;
        }
    }

    // When other objects leave the zone
    private void OnTriggerExit(Collider other)
    {
        // dont have to check for crossing zone because crossing zone collider will get disabled instead of exiting
        if (other.gameObject.tag == "Normal Car" || other.gameObject.tag == "Auto Car" || other.gameObject.tag == "Player")
        {
            carEngineScript.stoppingForSomething = false;
            carEngineScript.isBraking = false;
        }
    }

    private void OnDisable()
    {
        carEngineScript.isBraking = false;
        carEngineScript.stoppingForSomething = false;
    }
}
