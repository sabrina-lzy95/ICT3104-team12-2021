using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianLightController : MonoBehaviour
{
    public Light PgreenLight;
    public Light PredLight;
    public float LightCount;
    public enum state
    {
        red = 0,  green = 1
    }
    public state LightState = state.red;
    
    public bool emergency;
    //In case of emergency. Toggle.

    void Update()
    {
        if (!emergency) 
        {
            LightCount -= Time.deltaTime; //Count lightCount down to 0
            PredLight.enabled = LightState == state.red;
            PgreenLight.enabled = LightState == state.green;

            //This is what we do. When light count to 0
            if (LightCount <= 0)
            {
                //Add light state up. By 1
                LightState += 1;
                //It act like int. So it will go up more then 3 and we have to cap it down to 0. But it not really int. We have to cast it.
                if ((int)LightState > 1)
                {
                    LightState = 0;
                }
                //After we add LightState up. We start counting again. If we at yellow light? Give it 2 Seconds.
                //If we at red or green light? Give random number between 20 - 50 seconds
                if (LightState == state.green)
                {
                    //LightCount = Random.Range(20, 50);
                    LightCount = 12;
                    
                }
                else if (LightState == state.red)
                {
                    //LightCount = Random.Range(20, 50);
                    LightCount = 10;
                }
            }
        }
        else //Do this if emergency.
        {
            PgreenLight.enabled = true;

            PredLight.enabled = false;
        }
    }

    void OnTriggerEnter()
    {
        emergency = true;
    }
    //When leave trigger
    void OnTriggerExit()
    {
        emergency = false;
    }
}
