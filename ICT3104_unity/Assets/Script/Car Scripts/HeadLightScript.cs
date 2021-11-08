using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLightScript : MonoBehaviour
{
    private GameObject lightObject;
    private Light light;

    Light headlight;

    // Start is called before the first frame update
    void Start()
    {
        lightObject = GameObject.Find("Directional Light");
        light = lightObject.GetComponent<Light>();
        headlight = gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (light.enabled)
        {
            headlight.intensity = 0;
        }
        else
        {
            headlight.intensity = 2;
        }
    }
}
