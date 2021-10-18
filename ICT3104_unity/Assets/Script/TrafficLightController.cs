using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    public Light greenLight;
    public Light yellowLight;
    public Light redLight;
    //Instead of make 3 light count. Just make only one.
    public float LightCount;
    public enum state
    {
        red = 0, yellow = 1, green = 2, yellow2 = 3
    }//Here is light state. Also. Reason why we have 2 yellow? Because enum can act. Like int. And so if we want to toggle to next state. We just use + 1 on it. And it will be weird if it just go from red to yellow to green. Then immediately red.
    public state LightState = state.yellow;
    //Light state. Default yellow.
    public bool emergency;
    //In case of emergency. Toggle.

    void Update()
    {
        if (!emergency) // Are we have emergency? If not. Do this.
        {
            LightCount -= Time.deltaTime; //Count lightCount down to 0
            redLight.enabled = LightState == state.red;
            yellowLight.enabled = LightState == state.yellow || LightState == state.yellow2;
            greenLight.enabled = LightState == state.green;
            //This is a shorter version of 
           
            //This is what we do. When light count to 0
            if (LightCount <= 0)
            {
                //Add light state up. By 1
                LightState += 1;
                //It act like int. So it will go up more then 3 and we have to cap it down to 0. But it not really int. We have to cast it.
                if ((int)LightState > 3)
                {
                    LightState = 0;
                }
                //After we add LightState up. We start counting again. If we at yellow light? Give it 2 Seconds.
                //if (LightState == state.yellow || LightState == state.yellow2)
                if (LightState == state.yellow2)
                {
                    LightCount = 2;
                }
                //If we at red or green light? Give random number between 20 - 50 seconds
                else if (LightState == state.red || LightState == state.green)
                {
                    //LightCount = Random.Range(20, 50);
                    LightCount = Random.Range(05, 10);
                }
            }
        }
        else //Do this if emergency.
        {
            greenLight.enabled = true;
            yellowLight.enabled = false;
            redLight.enabled = false;
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
