using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Streetlight : MonoBehaviour
{
	Light light;

	// Use this for initialization
	void Start()
	{
		light = GetComponent<Light>();
	}

	// Update is called once per frame
	void Update()
	{
		// Toggle light on/off when L key is pressed.
		if (Input.GetKeyUp(KeyCode.Y))
		{
			light.enabled = !light.enabled;
		}
	}
}
