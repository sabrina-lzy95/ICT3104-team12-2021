using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCarSensor : MonoBehaviour
{
    public GameObject car;

    private CarEngine carEngineScript;
    private AudioSource carHorn;
    private bool playAudio = true;

    // Start is called before the first frame update
    void Start()
    {
        carEngineScript = car.GetComponent<CarEngine>();
        carHorn = car.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    // When other objects is within the zone
    private void OnTriggerStay(Collider other)
    {
        CheckForCollision(other);
        CheckForCrossingBarrier(other);
        CarToPlayerCommunication(other);
    }

    // When other objects leave the zone
    private void OnTriggerExit(Collider other)
    {
        // dont have to check for crossing zone because crossing zone collider will get disabled instead of exiting
        if (other.gameObject.tag == "Normal Car" || other.gameObject.tag == "Auto Car")
        {
            carEngineScript.isBraking = false;
            carEngineScript.stoppingForCar = false;
        }
        else if (other.gameObject.tag == "Player")
        {
            carEngineScript.isBraking = false;
            carEngineScript.stoppingForPlayer = false;
            playAudio = true; // reset the horn so can play again next tmr player enter.
        }
    }

    private void CheckForCollision(Collider other)
    {
        if (other.gameObject.tag == "Normal Car" || other.gameObject.tag == "Auto Car")
        {
            carEngineScript.isBraking = true;
            carEngineScript.stoppingForCar = true;
        }
        else if (other.gameObject.tag == "Player")
        {
            carEngineScript.isBraking = true;
            carEngineScript.stoppingForPlayer = true;
        }
    }

    private void CheckForCrossingBarrier(Collider other)
    {
        if (other.gameObject.tag == "Zebra Crossing Barrier")
        {
            // Randomly decide to stop or not. giveWayAtZebra is randomized in carEngine
            if (carEngineScript.giveWayAtZebra == 1)
            {
                carEngineScript.isBraking = true;
                carEngineScript.stoppingAtCrossing = true;
            }
        }
        else if (other.gameObject.tag == "Traffic Light Barrier")
        {
            carEngineScript.isBraking = true;
            carEngineScript.stoppingAtCrossing = true;
        }
    }

    private void CarToPlayerCommunication(Collider other)
    {
        // Horn the player if they do not have right of way
        if (carEngineScript.stoppingAtCrossing == false && other.gameObject.tag == "Player")
        {
            if (playAudio == true)
            {
                carHorn.Play();
            }
            playAudio = false;
        }
    }

    private void OnDisable()
    {
        carEngineScript.isBraking = false;
        carEngineScript.stoppingAtCrossing = false;
        carEngineScript.stoppingForCar = false;
        carEngineScript.stoppingForPlayer = false;
    }
}
