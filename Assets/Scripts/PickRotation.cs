using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickRotation : MonoBehaviour
{
    public GameObject cue;

    void FixedUpdate()
    {
        MoveAimingPointSprite();
        MoveCue();
    }

    public void ResetRotation()
    {
        transform.localPosition = Vector3.zero;
    }

    void MoveAimingPointSprite()
    {
        int movingSpeed = 40;
        if (Input.GetKey(KeyCode.LeftControl))
            movingSpeed = movingSpeed / 5;

        if (Input.GetKey("w") && transform.localPosition.y <= 25)
            transform.Translate(0, movingSpeed * Time.deltaTime, 0);

        if (Input.GetKey("s") && transform.localPosition.y >= -25)
            transform.Translate(0, -movingSpeed * Time.deltaTime, 0);
    }

    void MoveCue()
    {
        Quaternion rotation = Quaternion.Euler(0, 90, 0);
        Vector3 position = new Vector3(cue.transform.position.x, transform.localPosition.y/2 + 25, 0);
        cue.transform.SetPositionAndRotation(position, rotation);
    }
}
