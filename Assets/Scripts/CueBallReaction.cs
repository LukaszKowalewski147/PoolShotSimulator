using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBallReaction : MonoBehaviour
{
    public GameObject soundManager;
    public GameObject cueStick;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "CueStick" && !cueStick.GetComponent<Stroke>().IsAfterImpact())
        {
            GetComponent<CueBallMovement>().GetHit();
            soundManager.GetComponent<SoundManager>().PlayShotSound();
            cueStick.GetComponent<Stroke>().InformAboutImpact();
        }
        if(collider.tag == "Cushion")
            GetComponent<CueBallMovement>().ResetCueBallPosition(true);
    }
}
