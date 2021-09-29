using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour
{
    //Drag path parent 
    public Transform path;
    public float maxSteerAngle = 45f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public float maxMotorTorque = 80f;
    public float currentSpeed;
    public float maxSpeed = 100f;
    public Vector3 centerOfMass;

    /*[Header("Sensors")]
    public float sensorLength = 5f;
    public float frontSensorPosition = 0.5f;*/

    //Store all the nodes of the path
    private List<Transform> nodes;
    private int currentNode = 0;

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
        //Sensors();
        ApplySteer();
        Drive();
        CheckWaypointDistance();
    }

    /*private void Sensors()
    {
        RaycastHit hit;
        Vector3 sensorStartPos = transform.position;
        sensorStartPos.z += frontSensorPosition;

        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {

        }
        Debug.DrawLine(sensorStartPos, hit.point);
    }*/

    private void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
    }

    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;
        if (currentSpeed < maxSpeed)
        {
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }
        
    }

    private void CheckWaypointDistance()
    {
        if(Vector3.Distance(transform.position, nodes[currentNode].position) < 0.5f)
        {
            if(currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
    }
}
