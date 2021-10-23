using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour
{
    public Transform path;
    public float maxSteerAngle = 45f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public float maxMotorTorque = 80f;
    public float currentSpeed;
    public float maxSpeed = 100f;
    public Vector3 centerOfMass;

    //Store all the nodes of the path
    private List<Transform> nodes;
    //Keep track of our current node
    private int currentNode = 0;
    // tell the car to apply brakes
    public bool stop = false;

    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;
        
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

    private void FixedUpdate()
    {
        ApplySteer();

        if (stop == false)
        {
            Drive();
        }
        else
        {
            Brake();
        }

        CheckWaypointDistance();
    }

    //Calculation for the wheel turning the right direction depending on where the next waypoint.
    private void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        //Magnitude is the length of the vector 
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
    }

    //Calculation for autonomous driving towards the waypoint
    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;
        if (currentSpeed < maxSpeed)
        {
            //motorTorque is for the engine of the car wheels
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }
        
    }

    private void Brake()
    {
        // Cut engine power
        wheelFL.motorTorque = 0;
        wheelFR.motorTorque = 0;

        // Apply brakes
        wheelFL.brakeTorque = 300;
        wheelFR.brakeTorque = 300;
    }

    //Calculate the distance towards the node and if is very close to the node it will go to the next one.
    //Therefore increase the current node. 
    private void CheckWaypointDistance()
    {
        //if the distance to the way point is smaller than a number, we will go to set the current node to the next node
        if(Vector3.Distance(transform.position, nodes[currentNode].position) < 0.9f)
        {
            //if it is the last node, will set the current node to zero
            if(currentNode == nodes.Count - 1)
            {
                //currentNode = 0;
                stop = true;
            }
            //if it is not the last one, will increase the current node
            else
            {
                currentNode++;
            }
        }
    }
}
