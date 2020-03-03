using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnWorld : MonoBehaviour {

    public Transform player;
    
    Vector3 spawnLocation;
    new MeshCollider collider;
    

    void Start()
    {
        collider = GetComponent<MeshCollider>();
    }

    bool isLocationSpawneable(Vector3 spawnLocation)
    {
        bool canSpawn;

        Collider[] cols = Physics.OverlapSphere(spawnLocation, 1f, ~(1 << LayerMask.NameToLayer("World")));
        canSpawn = cols.Length == 0;

        return canSpawn;
    }
    Vector3 determineSpawnLocation(float distFromWorld, GameObject spawningObject)
    {
        Vector3 sphereVector;

        Vector3 sphereUnitVec = Random.onUnitSphere;

        sphereVector = sphereUnitVec * collider.bounds.extents.x + sphereUnitVec 
        * (distFromWorld + (spawningObject.GetComponent<Collider>().bounds.extents.y/2));

        // sphereVector = sphereUnitVec * (collider.bounds.extents.x + distFromWorld);

        return transform.position + sphereVector;
    }

    /// <summary>
    /// spawns objects out of the field of view of the player
    /// I'm going to change this, as we discussed we can spawn seeds at first 
    /// and then have the food spawn after some time, so we don't have to waste
    /// processing if the spawn point is out of view
    /// </summary>
    /// <param name="spawningObject"></param>
    /// <param name="distFromWorld"></param>

    public void spawnObject(GameObject spawningObject, float distFromWorld, bool setActive = true, bool notWithinView = false)
    {
        float cosine;

        do
        {
            if(notWithinView)
                do
                {
                    spawnLocation = determineSpawnLocation(distFromWorld, spawningObject);
                    cosine = Vector3.Dot((player.position - transform.position).normalized, (spawnLocation - transform.position).normalized);

                } 
                while (cosine > -0.36);
            
            else
                spawnLocation = determineSpawnLocation(distFromWorld, spawningObject); 

        } while (!isLocationSpawneable(spawnLocation));


        spawningObject.transform.position = spawnLocation;
        spawningObject.transform.parent = transform;

        spawningObject.SetActive(setActive);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(spawnLocation, 1f);
    }
}
