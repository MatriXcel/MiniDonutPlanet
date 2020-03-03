using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float forwardSpeed;

    private Rigidbody rb;
    public VirtualJoystick vJoystick;

    public FillBar fillBar;
    
    Vector3 joystickPos;

    public bool isMoveable;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        fillBar.onBarCompleted += die; //kill the player if bar is full
        fillBar.onBarDrained += die; //kill the player if bar is drained
    }

    void die()
    {
        //player killed, call GameManager.Instance.GameOver();
    }

    void FixedUpdate()
    {
        //good work, keep at it
        if (isMoveable)
        {
            rb.MovePosition(rb.position + (transform.forward * forwardSpeed * Time.fixedDeltaTime));
        }

        
        /*
        the standardRot is the rotation the player would have if it were not rotated
        about its upVector, we need to have this default or else there would be no 
        reference frame for the joystick to rotate off of
         */
        Quaternion standardRot = transform.rotation * ((joystickPos != Vector3.zero) ? Quaternion.Inverse(Quaternion.LookRotation(joystickPos)) : Quaternion.identity);

        //get the x,y position of the joystick
        joystickPos = new Vector3(vJoystick.Horizontal(), 0.0f, vJoystick.Vertical());

        if (joystickPos != Vector3.zero)
            //rotate to the desired orientation
            rb.MoveRotation(standardRot * Quaternion.LookRotation(joystickPos));
    }
}
