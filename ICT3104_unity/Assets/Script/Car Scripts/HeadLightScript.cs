using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLightScript : MonoBehaviour
{
    private bool isItDayTime = true;

    Light headlight;

    // Start is called before the first frame update
    void Start()
    {
        headlight = gameObject.GetComponent<Light>();
        headlight.intensity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.inputString == "y")
        {
            if (isItDayTime)
            {
                headlight.intensity = 2;
                isItDayTime = false;
            }
            else
            {
                headlight.intensity = 0;
                isItDayTime = true;
            }
        }
    }
}
