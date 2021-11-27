using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCarSensor : MonoBehaviour
{
    public GameObject car;
    public GameObject go_sign;
    public GameObject stop_sign;
    private AudioSource carHorn;
    private bool playAudio = true;

    private CarEngine carEngineScript;

    // Start is called before the first frame update
    void Start()
    {
        carEngineScript = car.GetComponent<CarEngine>();
        carHorn = car.GetComponent<AudioSource>();
        go_sign.SetActive(false);
        stop_sign.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // stop dislaying go sign once car is not stopping at crossing
        if (carEngineScript.stoppingAtCrossing == false)
        {
            go_sign.SetActive(false);
        }
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
            stop_sign.SetActive(false);
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
        if (other.gameObject.tag == "Zebra Crossing Barrier" || other.gameObject.tag == "Traffic Light Barrier")
        {
            carEngineScript.isBraking = true;
            carEngineScript.stoppingAtCrossing = true;
        }
    }

    private void CarToPlayerCommunication(Collider other)
    {
        // show stop sign if player do not have right of way
        if (carEngineScript.stoppingAtCrossing == false && other.gameObject.tag == "Player")
        {
            stop_sign.SetActive(true);
            if (playAudio == true)
            {
                carHorn.Play();
            }
            playAudio = false;
        }
        // show go sign if car is stopped at crossing
        else if (carEngineScript.stoppingAtCrossing == true)
        {
            go_sign.SetActive(true);
        }
    }

    private void OnDisable()
    {
        carEngineScript.isBraking = false;
        carEngineScript.stoppingForCar = false;
        carEngineScript.stoppingForPlayer = false;
    }
}
