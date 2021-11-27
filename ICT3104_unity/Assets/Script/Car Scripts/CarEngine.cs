using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CarEngine : MonoBehaviour
{
    public Transform path;
    public float maxSteerAngle = 70f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;
    public GameObject sensorFar;
    public GameObject sensorMedium;
    public GameObject sensorLeft;
    public GameObject sensorRight;
    public float maxBrakeTorque = 500f;
    public float maxMotorTorque = 150f;
    public float currentSpeed;
    public float maxSpeed = 10f;
    public Vector3 centerOfMass;
    public bool isBraking = false;
    public bool stoppingForCar = false;
    public bool stoppingForPlayer = false;
    public bool stoppingAtCrossing = false;
    public int giveWayAtZebra;

    //Store all the nodes of the path
    private List<Transform> nodes;
    //Keep track of our current node
    private int currentNode = 0;
    // car's rigidbody
    private Rigidbody rb;
    private float currentAngle;
	private bool currentStopForPlayer = false;
	private bool currentStopForCar = false;
	private bool currentStopForCrossing = false;
	DateTime localDate;

    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;

        // original cars dont have paths assigned to them, only cloned cars have paths assigned upon instantiating.
        // check not null to prevent error messages
        if (path != null) 
        {
            //GetComponentsInChildren find all the child objects
            Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();

            //Making sure our list is empty at the beginning, so we set this to a new list
            nodes = new List<Transform>();

            //Looping through the array
            for (int i = 0; i < pathTransforms.Length; i++)
            {
                //If the transform is not our own transform, 
                if (pathTransforms[i] != path.transform)
                {
                    //we're going to add it to the node array which node array only contains our child nodes
                    nodes.Add(pathTransforms[i]);
                }
            }
        }
        
        // get the rigid body of the car so can get speed
        rb = gameObject.GetComponent<Rigidbody>();
        // random value for give way, if its 1, this car will give way at zebra crossings.
        giveWayAtZebra = UnityEngine.Random.Range(0, 2);
    }

    private void FixedUpdate()
    {
		localDate = DateTime.Now;
		if(currentStopForPlayer == false && stoppingForPlayer == true){
			PlayerPrefs.SetString ("log",PlayerPrefs.GetString("log")+"\n"+localDate.ToString()+","+"Car stopped for player");
			currentStopForPlayer = true;
		}
		if(currentStopForCar == false && stoppingForCar == true){
			PlayerPrefs.SetString ("log",PlayerPrefs.GetString("log")+"\n"+localDate.ToString()+","+"Car stopped for car");
			currentStopForCar = true;
		}
		if(currentStopForCrossing == false && stoppingAtCrossing == true){
			PlayerPrefs.SetString ("log",PlayerPrefs.GetString("log")+"\n"+localDate.ToString()+","+"Car stopped for crossing");
			currentStopForCrossing = true;
		}
        currentSpeed = rb.velocity.magnitude; // speed of car

        // original cars dont have paths assigned to them, only cloned cars have paths assigned upon instantiating.
        // check not null to prevent error messages
        if (path != null)
        {
            ApplySteer();
            CheckWaypointDistance();
        }

        Drive();
        Braking();
        Sensors();
    }

    //Calculation for the wheel turning the right direction depending on where the next waypoint.
    private void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        //Magnitude is the length of the vector 
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
        currentAngle = newSteer;
    }

    //Calculation for autonomous driving towards the waypoint
    private void Drive()
    {
        // currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;
        if (currentSpeed < maxSpeed && !isBraking)
        {
            //motorTorque is for the engine of the car wheels
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
            wheelRL.motorTorque = maxMotorTorque;
            wheelRR.motorTorque = maxMotorTorque;
        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
            wheelRL.motorTorque = 0;
            wheelRR.motorTorque = 0;
        }
        
    }

    private void Braking()
    {
        if (isBraking)
        {


            wheelFL.brakeTorque = maxBrakeTorque;
            wheelFR.brakeTorque = maxBrakeTorque;
            wheelRL.brakeTorque = maxBrakeTorque;
            wheelRR.brakeTorque = maxBrakeTorque;
        } 
        else
        {
			currentStopForPlayer = false;
			currentStopForCar = false;
			currentStopForCrossing = false;
            wheelFL.brakeTorque = 0;
            wheelFR.brakeTorque = 0;
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
        }
    }

    private void Sensors()
    {
        // turning right
        if (currentAngle >= 10)
        {
            sensorFar.SetActive(false);
            sensorMedium.SetActive(false);
            sensorLeft.SetActive(false);
            sensorRight.SetActive(true);
        }
        // turning left
        else if (currentAngle <= -10)
        {
            sensorFar.SetActive(false);
            sensorMedium.SetActive(false);
            sensorLeft.SetActive(true);
            sensorRight.SetActive(false);
        }
        // going straight
        else
        {
            if (currentSpeed > 8)
            {
                sensorFar.SetActive(true);
                sensorMedium.SetActive(true);
            }
            else if (currentSpeed > 5)
            {
                sensorFar.SetActive(false);
                sensorMedium.SetActive(true);
            }
            else
            {
                sensorFar.SetActive(false);
                sensorMedium.SetActive(false);
            }
            sensorLeft.SetActive(false);
            sensorRight.SetActive(false);
        }
    }

    //Calculate the distance towards the node and if is very close to the node it will go to the next one.
    //Therefore increase the current node. 
    private void CheckWaypointDistance()
    {
        //if the distance to the way point is smaller than a number, we will go to set the current node to the next node
        if(Vector3.Distance(transform.position, nodes[currentNode].position) < 0.9f)
        {
            //if it is the last node, will set the current node to zero
            if (currentNode == nodes.Count - 1)
            {
                //currentNode = 0;
            }
            //if it is not the last one, will increase the current node
            else
            {
                currentNode++;
            }
        }
    }
}
