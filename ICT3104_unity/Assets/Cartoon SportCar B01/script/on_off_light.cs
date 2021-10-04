using UnityEngine;
using System.Collections;

public class on_off_light : MonoBehaviour
{

	public Light[] lights;
	public KeyCode keyboard;


	void Update ()
	{

		foreach (Light light in lights)
		{
			var car = GameObject.FindGameObjectWithTag("Player").transform;
			var human = GameObject.FindGameObjectWithTag("human").transform;
			//	if (Input.GetKeyDown(keyboard))
			Vector3 directionToTarget = car.position - human.position;
			float angle = Vector3.Angle(car.forward, directionToTarget);
			float distance = directionToTarget.magnitude;
			 
			if (Mathf.Abs(angle) > 90 && distance < 5){
				Debug.Log("target is in front of me");
					if(light.enabled == false)
						light .enabled = !light .enabled;
			}
			else{
					if(light.enabled == true)
						light .enabled = !light .enabled;
			}
		}
	}
}

