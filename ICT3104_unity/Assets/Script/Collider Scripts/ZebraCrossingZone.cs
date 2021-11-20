using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZebraCrossingZone : MonoBehaviour
{
    public bool playerIsCrossing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerIsCrossing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerIsCrossing = false;
        }
    }
}
