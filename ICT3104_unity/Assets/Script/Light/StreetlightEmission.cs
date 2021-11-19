using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetlightEmission : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            // toggle visibility:
            GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
        }
    }
}
