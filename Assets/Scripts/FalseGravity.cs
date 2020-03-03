using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FalseGravity : MonoBehaviour {

    //strength of the gravitational force
    public float gravForce;

	void Start () {

	}


	void FixedUpdate () {
        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
        {
            //if an object in the scene has a rigidbody, apply a force to it
            if (obj != this.gameObject && obj.GetComponent<Rigidbody>() != null)
            {
                attract(obj.transform);
            }
        }
    }

    void attract(Transform obj)
    {

        Rigidbody rb = obj.GetComponent<Rigidbody>();

        rb.freezeRotation = true;
        rb.useGravity = false;

        /*
        the normalized distance vector which extends from the center of the planet to the 
        object the gravity is being applied to.
         */
        Vector3 directionNorm = (transform.position - obj.position).normalized;

        //add a force in the direction towards the center of the planet
        rb.AddForce(directionNorm *  gravForce, ForceMode.Acceleration);
        Debug.DrawLine(transform.position, obj.position);

        //rotate the upVector of the object to match the direction in which gravity is pulling it
        Quaternion toRotation = Quaternion.FromToRotation(obj.transform.up, -directionNorm) * obj.transform.rotation;
        obj.transform.rotation = toRotation;
    
        

    }


}
