using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFoodFromSeed : MonoBehaviour
{
    void OnEnable()
    {
        //spawns food after 5 seconds
        Invoke("spawnFood", 5);
    }

    void spawnFood()
    {
        //get food from the spawner
        GameObject foodObj = FoodSpawner.Instance.GetFood();
        foodObj.SetActive(true);
        
        //set its position to where the seed was
        foodObj.transform.position = transform.position;

        //remove the seed
        this.gameObject.SetActive(false);
        this.gameObject.transform.parent = FoodSpawner.Instance.transform;
    }
}
