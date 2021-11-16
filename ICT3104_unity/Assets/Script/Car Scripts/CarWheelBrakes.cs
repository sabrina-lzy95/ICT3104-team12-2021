using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWheelBrakes : MonoBehaviour
{
    public WheelCollider targetWheel;
    private Vector3 wheelPosition = new Vector3();
    private Quaternion wheelRotation = new Quaternion();



    void Update()
    {
        targetWheel.GetWorldPose(out wheelPosition, out wheelRotation);
        transform.position = wheelPosition;
        transform.rotation = new Quaternion(0, wheelRotation.y, 0, wheelRotation.w);
    }
}
