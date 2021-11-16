using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZoneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When other objects enter the zone
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject.transform.parent.gameObject);
        if (other.gameObject.tag == "Normal Car" || other.gameObject.tag == "Auto Car") // Checks if the object that entered the collider is a car
        {
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }
}
