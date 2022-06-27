using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickForce : MonoBehaviour
{
    public GameObject cue;
    public Slider powerBar;
    int frameCounter;
    int movingSpeed = 4;
    
    void Start()
    {
        frameCounter = 0;
    }

    void FixedUpdate()
    {
        if (++frameCounter % 3 == 0)
        {
            frameCounter = 0;

            if (Input.GetKey(KeyCode.LeftControl))
                movingSpeed = 1;

            if (Input.GetKey("a"))
                powerBar.value -= movingSpeed;
            if (Input.GetKey("d"))
                powerBar.value += movingSpeed;
            movingSpeed = 4;
        }
    }
}
