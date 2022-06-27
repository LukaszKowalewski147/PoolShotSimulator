using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Stroke : MonoBehaviour
{
    public Slider forcePicker;
    public SphereCollider cueTipCollider;
    public GameObject aimPicker;
    Rigidbody cueStick;
    int frameCounter;
    float shotForce;

    bool playingShot = false;
    bool backSwingDone = false;
    bool pauseDone = false;
    bool followThroughDone = false;
    bool pauseAfterShotDone = false;
    bool cueTakenBack = false;
    bool afterImpact = false;
    bool cueIsMoving = false;

    void Start()
    {
        cueStick = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !playingShot)
        {
            playingShot = true;
            cueTipCollider.enabled = true;
            shotForce = forcePicker.value;
            Debug.Log("shot force = " + shotForce.ToString());
            aimPicker.GetComponent<PickRotation>().enabled = false;
            forcePicker.GetComponent<PickForce>().enabled = false;
        }
        if (playingShot)
            PlayShot();
    }

    public bool IsAfterImpact()
    {
        if (afterImpact)
            return true;
        return false;
    }

    public void InformAboutImpact()
    {
        afterImpact = true;
    }

    void PlayShot()
    {
        if (!backSwingDone)
            BackSwing();
        if (!pauseDone && backSwingDone)
            Pause(false);
        if (!followThroughDone && pauseDone)
            FollowThrough();
        if (!pauseAfterShotDone && followThroughDone)
            Pause(true);
        if (!cueTakenBack && pauseAfterShotDone)
            TakeCueBack();
        if (cueTakenBack)
            ResetVariables();
    }

    void StartMovingCue(float speed)
    {
        cueStick.velocity = new Vector3(speed, 0, 0);
        cueIsMoving = true;
    }

    void BackSwing()
    {
        if (!cueIsMoving)
            StartMovingCue((10 + shotForce * 3) * -1);
        if (transform.position.x <= -660 - (shotForce * 2))
        {
            backSwingDone = true;
            cueIsMoving = false;
            cueStick.velocity = Vector3.zero;
        }
    }

    void Pause(bool afterShot)
    {
        int timeToWait = 30;
        if (afterShot)
            timeToWait *= 2;
        if (++frameCounter == timeToWait)
        {
            frameCounter = 0;
            if (afterShot)
                pauseAfterShotDone = true;
            else
                pauseDone = true;
        }
    }

    void FollowThrough()
    {
        if (!cueIsMoving)
            StartMovingCue(50 + shotForce * 20);
        if (transform.position.x >= -630 + (shotForce * 2))
        {
            followThroughDone = true;
            cueIsMoving = false;
            cueStick.velocity = Vector3.zero;
        }
    }

    void TakeCueBack()
    {
        if (!cueIsMoving)
            StartMovingCue(-1000);
        if (transform.position.x <= -650)
        {
            transform.position = new Vector3(-650, transform.position.y, 0);
            cueTakenBack = true;
            cueIsMoving = false;
            cueStick.velocity = Vector3.zero;
        }
    }

    void ResetVariables()
    {
        playingShot = false;
        backSwingDone = false;
        pauseDone = false;
        followThroughDone = false;
        pauseAfterShotDone = false;
        cueTakenBack = false;
        afterImpact = false;
        cueIsMoving = false;
        aimPicker.GetComponent<PickRotation>().enabled = true;
        forcePicker.GetComponent<PickForce>().enabled = true;
        cueTipCollider.enabled = true;
    }
}
