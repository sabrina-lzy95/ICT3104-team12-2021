using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLights : MonoBehaviour
{
    public Material lightsOnMaterial;
    public Material lightsOffMaterial;

    private GameObject lightObject;
    private Light light;

    // Start is called before the first frame update
    void Start()
    {
        lightObject = GameObject.Find("Directional Light");
        light = lightObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (light.enabled)
        {
            gameObject.GetComponent<Renderer>().material = lightsOffMaterial;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = lightsOnMaterial;
        }
    }
}
