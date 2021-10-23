using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3Controller : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject normalCar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            spawnNormalCar();
        }
    }

    private void spawnNormalCar()
    {
        // Instantiate at position (0, 0, 0) and zero rotation.
        Instantiate(normalCar, new Vector3(19, 0, -55), Quaternion.Euler(0, 270, 0));
    }
}
