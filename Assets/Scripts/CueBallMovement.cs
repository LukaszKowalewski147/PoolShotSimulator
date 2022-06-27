using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CueBallMovement : MonoBehaviour
{
    public GameObject cue;
    public GameObject aimPicker;
    public Slider forcePicker;
    Rigidbody cueBall;

    // masa bili =    170g = 0.17kg
    // promień bili = 57mm = 0.057m

    double radius = 25.3;
    double mass;
    double gravitationalAcceleration = 9.8f;
    double coefficientOfRollingFrictionWithCloth = 300.0f;
    double coefficientOfSlidingFrictionWithCloth = 1000.0f;
    double rollingFriction;
    double slidingFriction;

    // Start is called before the first frame update
    void Start()
    {
        cueBall = GetComponent<Rigidbody>();
        cueBall.maxAngularVelocity = 100;
        mass = cueBall.mass;

        double normalForce = gravitationalAcceleration * mass;

        rollingFriction = (coefficientOfRollingFrictionWithCloth * normalForce) / radius;
        slidingFriction = coefficientOfSlidingFrictionWithCloth * normalForce;

        Debug.Log("mass: " + cueBall.mass.ToString());
        Debug.Log("norm: " + normalForce.ToString());
        Debug.Log("frol: " + rollingFriction.ToString());
        Debug.Log("srol: " + slidingFriction.ToString());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyFrictionForce();
        ResetCueBallPosition(false);
        //MoveCueBall();
        //ShowSpeed();
        //ShowInfo();
    }

    public void GetHit()
    {
        float speed = cue.GetComponent<Rigidbody>().velocity.magnitude;
        Debug.Log("speed by velocity: " + speed.ToString());
        float maxSpin = speed / (float)radius;
        float spinAmount = aimPicker.transform.localPosition.y;
        if (spinAmount > 25)
            spinAmount = 25;
        else if (spinAmount < -25)
            spinAmount = -25;
        float spinProportion = spinAmount * 4; // skrot z proporcji (Mathf.Abs(spinAmount) * 100) / 25
        float spinToApply = maxSpin * spinProportion / 100;
        cueBall.velocity = new Vector3(speed, 0, 0);
        cueBall.angularVelocity = new Vector3(0, 0, -spinToApply);
        Debug.Log("max spin: " + maxSpin.ToString());
        Debug.Log("spin applied: " + spinToApply.ToString());
        Debug.Log("spin aimed: " + spinAmount.ToString());
    }

    public void ResetCueBallPosition(bool triggeredByCushion)
    {
        if (Input.GetKey("q") || triggeredByCushion)
        {
            cueBall.velocity = Vector3.zero;
            cueBall.angularVelocity = Vector3.zero;
            cueBall.transform.position = new Vector3(0, 25.43f, 0);
            cueBall.transform.rotation = Quaternion.Euler(new Vector3(70, 10, 90));
        }
    }

    void MoveCueBall()
    {
        int force = 5000;
        if (Input.GetKey(KeyCode.LeftControl))
            force /= 3;
        if (Input.GetKey("r"))
            cueBall.AddForce(force * Time.deltaTime, 0, 0, ForceMode.VelocityChange);

        if (Input.GetKey("f"))
            cueBall.AddForce(-force * Time.deltaTime, 0, 0, ForceMode.VelocityChange);

        if (Input.GetKey("v"))
        {
            cueBall.AddForce(force * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            cueBall.AddTorque(0, 0, 150 * Time.deltaTime, ForceMode.VelocityChange);
        }
    }

    void ApplyFrictionForce()
    {
        if (cueBall.velocity.magnitude > 2)
        {
            if (CueBallIsSliding())
            {
                //Debug.Log("sliding");
                ApplySlidingFrictionForce();
            }
            else
            {
                ApplyRollingFrictionForce();
                //Debug.Log("rolling");
            }
                
        }
        else
        {
            if (cueBall.angularVelocity.magnitude * radius < 30)
            {
                cueBall.velocity = Vector3.zero;
                cueBall.angularVelocity = Vector3.zero;
            }  
        }
    }

    bool CueBallIsSliding()
    {
        double linearVelocity = cueBall.velocity.x;
        double velocityFromRotation = -cueBall.angularVelocity.z * radius;

        if ((velocityFromRotation < linearVelocity - 20) || (velocityFromRotation > linearVelocity + 20))
            return true;
        return false;
    }

    void ApplySlidingFrictionForce()
    {
        float frictionForce = (float)slidingFriction;

        if ((IsMovingForward() && !IsRollingFasterThanMoving()) || (!IsMovingForward() && IsRollingFasterThanMoving()))
            frictionForce = -frictionForce;
        cueBall.AddForce(frictionForce / 30 * Time.deltaTime, 0, 0, ForceMode.Impulse);

        frictionForce = (float)slidingFriction;

        if ((IsMovingForward() && !IsRollingFasterThanMoving()) || (IsMovingForward() && !IsRollingForward()) || (!IsMovingForward() && IsRollingFasterThanMoving()))
            frictionForce = -frictionForce;
        cueBall.AddTorque(0, 0, frictionForce * Time.deltaTime, ForceMode.Impulse);
    }

    void ApplyRollingFrictionForce()
    {
        float frictionForce = (float)rollingFriction;
        if (!IsMovingForward())
            frictionForce = -frictionForce;
        cueBall.AddForce(-frictionForce * Time.deltaTime, 0, 0, ForceMode.Impulse);
    }

    void ShowInfo()
    {
        if (cueBall.velocity.magnitude > 1)
        {
            double angularVelocity = -cueBall.angularVelocity.z;
            double angularVelocitymag = -cueBall.angularVelocity.magnitude * radius;
            double linearVelocity = cueBall.velocity.x;
            double velocityFromRotation = angularVelocity * radius;

            //Debug.Log("angular velocity: " + angularVelocity.ToString());
            Debug.Log("linear velocity: " + linearVelocity.ToString());
            Debug.Log("velocity from rotation: " + velocityFromRotation.ToString());
            //Debug.Log("vel mag  from rotation: " + angularVelocitymag.ToString());
        }
    }

    bool IsMovingForward()
    {
        if (cueBall.velocity.x > 0)
            return true;
        else
            return false;
    }

    bool IsRollingForward()
    {
        if (-cueBall.angularVelocity.z * radius > 2)
        {
            //Debug.Log("rolling forwards");
            return true;
        }
        else
        {
            //Debug.Log("rolling backwards");
            return false;
        }
    }

    bool IsRollingFasterThanMoving()
    {
        double linearVelocity = cueBall.velocity.x;
        double velocityFromRotation = -cueBall.angularVelocity.z * radius;

        if (IsMovingForward() && velocityFromRotation > linearVelocity + 3)
        {
            //Debug.Log("rolling faseter");
            return true;
        }
        else if (!IsMovingForward() && velocityFromRotation < linearVelocity - 3)
        {
            // Debug.Log("rolling faseter");
            return true;
        }
        //Debug.Log("rolling slower");
        return false;
    }

    void ShowSpeed()
    {
        if (cueBall.velocity.magnitude > 1)
        {
            float linearVelocity = cueBall.velocity.x;
            Debug.Log("V = " + linearVelocity.ToString());
        }
    }
}
