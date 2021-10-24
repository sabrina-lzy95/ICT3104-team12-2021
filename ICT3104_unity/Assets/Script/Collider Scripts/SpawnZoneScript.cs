using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZoneScript : MonoBehaviour
{
    public bool haveObjectInSpawnZone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When other objects is within the zone
    private void OnTriggerStay(Collider other)
    {
        haveObjectInSpawnZone = true;
    }

    // When other objects leave the zone
    private void OnTriggerExit(Collider other)
    {
        haveObjectInSpawnZone = false;
    }
}
