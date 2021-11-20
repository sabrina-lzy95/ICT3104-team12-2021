using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZebraCrossingBarrierScript : MonoBehaviour
{
    public GameObject zebraCrossingZone;

    private ZebraCrossingZone zebraCrossingZoneScript;
    private Collider collider;
    private GameObject car;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        zebraCrossingZoneScript = zebraCrossingZone.GetComponent<ZebraCrossingZone>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (zebraCrossingZoneScript.playerIsCrossing)
        {
            collider.enabled = true;
        }
        else
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
