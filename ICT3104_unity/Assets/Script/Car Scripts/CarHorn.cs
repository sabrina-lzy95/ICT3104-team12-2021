using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHorn : MonoBehaviour
{
    AudioSource aS;

    // Start is called before the first frame update
    void Start()
    {
        aS = GetComponent<AudioSource>();
    }

    //Play horn audio
    void Update()
    {
        if (Input.GetKey(KeyCode.H))
        {
            if (!aS.isPlaying)
            {
                aS.loop = true;
                aS.Play();
            }
        }
        else
        {
            aS.Stop();
        }
    }

}
