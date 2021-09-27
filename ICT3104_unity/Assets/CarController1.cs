using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController1 : MonoBehaviour
{
    //preserve the start position and start rotation at the beginning
    //everytime the car dies or crashes it will reset back to this position
    private Vector3 startPosition, startRotation;

    [Range(-1f, 1f)]
    //variable for acceleration and turning values
    public float a, t;

    //keep track of how long it's been since the car has started.
    //Use this to check if the car has been idle for too long and if its been idle that's mean that the car is useless
    //so just reset the network 
    public float timeSinceStart = 0f;

    //How well a car or like an entity does in its run 
    //How far it goes and how fast it is = the score of the car 
    [Header("Fitness")]
    public float overallFitness;
    //how important the distance is to the fitness function
    public float distanceMultipler = 1.4f;
    //how important the speed is to the overall fitness
    public float avgSpeedMultiplier = 0.2f;

    //used to calculate the fitness
    private Vector3 lastPosition;
    private float totalDistanceTravelled;
    private float avgSpeed;

    //Each of these floats just contains a distance of the origin between the origin of the ray to the
    //position where the ray hit the wall. Inputs to neural net
    private float aSensor, bSensor, cSensor;

    private void Awake()
    {
        startPosition = transform.position;
        startRotation = transform.eulerAngles;
    }

    public void Reset()
    {
        timeSinceStart = 0f;
        totalDistanceTravelled = 0f;
        avgSpeed = 0f;
        lastPosition = startPosition;
        overallFitness = 0f;
        transform.position = startPosition;
        transform.eulerAngles = startRotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Reset();
    }

    private Vector3 inp;
    //vertical and horizontal movement
    public void MoveCar(float v, float h)
    {
        inp = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, v * 11.4f), 0.02f);
        inp = transform.TransformDirection(inp);
    }
}
