using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCarReaction : MonoBehaviour
{
    private CarEngine carEngineScript;
    private AudioSource carHorn;

    private GameObject car;


    void Start()
    {
        carEngineScript = GetComponent<CarEngine>();
        carHorn = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (carEngineScript.stoppingAtCrossing == false && other.gameObject.tag == "Player")
        {
            carHorn.Play();
        }
    }
}
