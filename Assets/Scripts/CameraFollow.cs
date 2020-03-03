using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player;

    public float smoothSpeed;
    public float distFromPlayer;

	void Start () {
        
	}
	
	
	void Update () {


        /*
        third person view
         */
        transform.position = player.transform.position + player.transform.up * distFromPlayer;

        /*
        make the camera's forward vector point in the direction of the player's -upVector
         */
        transform.rotation = Quaternion.FromToRotation(transform.forward, -player.transform.up) * transform.rotation;

        //make the camera's forward vector point where the player is facing towards
        Quaternion toRotation = (Quaternion.FromToRotation(transform.up, player.transform.forward) * transform.rotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * smoothSpeed);

    }

}
