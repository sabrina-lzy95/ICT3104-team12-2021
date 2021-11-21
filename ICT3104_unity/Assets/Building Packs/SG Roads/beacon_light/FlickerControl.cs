using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerControl : MonoBehaviour
{
    public Material lightsOnMaterial;
    public Material lightsOffMaterial;
    private bool isFlickering = false;

    // Update is called once per frame
    void Update()
    {
        if (isFlickering == false)
        {
            StartCoroutine(FlickeringLight());
        }
    }

    IEnumerator FlickeringLight()
    {
        isFlickering = true;

        //turn off the light 
        gameObject.GetComponent<Renderer>().material = lightsOffMaterial;
        //delay for 1 sec
        yield return new WaitForSeconds(1);

        //turn on the light
        gameObject.GetComponent<Renderer>().material = lightsOnMaterial;
        //delay for 1 sec
        yield return new WaitForSeconds(1);

        //will keep restarting coroutine, because if statement = false
        isFlickering = false;

    }
}
