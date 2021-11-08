using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLightsScript : MonoBehaviour
{
    public Material lightsOnMaterial;
    public Material lightsOffMaterial;

    private bool isItDayTime = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.inputString == "y")
        {
            if (isItDayTime)
            {
                gameObject.GetComponent<Renderer>().material = lightsOnMaterial;
                isItDayTime = false;
            }
            else
            {
                gameObject.GetComponent<Renderer>().material = lightsOffMaterial;
                isItDayTime = true;
            }
        }
    }
}
