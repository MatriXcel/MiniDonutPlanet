using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This spawner is responsible for spawning foods
/// </summary>
/// <typeparam name="EntitySpawnInfo">
/// contains information like how many foods are in the world, 
/// how often to spawn them(threshold).
///  </typeparam>
/// <typeparam name="FoodType">
/// Specifices properties that are specific to a FoodType such as the name and image associated with the food.
/// </typeparam>

public sealed class FoodSpawner : EntitySpawner <FoodSpawner, EntitySpawnInfo, FoodType> {

    float currentTime; //keeps track of time
    float targetTime; //the next time food can potentially spawn

    public EntityType seedType; //we spawn seeds, and those seeds in turn spawn foods


    void Awake () {
        Instance = this;
	}
	

	void Update () {
        if (currentTime >= targetTime)
        {
            float test = Random.Range(0, 10); //pick a random number between 0 and 10

            /* as long as that number is less than the threshold and the current amount of foods
               is less than the maximum amount, then keep spawning foods
            */
            
            if (test < entityInfo.threshold && entityInfo.currAmt < entityInfo.maxAmt)
            {
                Spawn();
            }

            //update the interval, the next time food can potentially spawn
            targetTime = Time.time + entityInfo.spawnInterval;
        }
        currentTime += Time.deltaTime;
    }

    /// <summary>
    /// spawn the seed
    /// </summary>
    protected override void Spawn()
    {
        base.Spawn(seedType);
    }

    /// <summary>
    /// generates a random food and adds a food component to it and returns that
    /// </summary>
    /// <returns>the Food's GameObject</returns>
    public GameObject GetFood()
    {
        FoodType chosenFoodType = entityTypeList[Random.Range(0, entityTypeList.Length)];

        GameObject foodObj = factory.create(chosenFoodType);

        Food foodComp;
         if(foodObj.GetComponent<Food>() == null)
         {
             foodComp = foodObj.AddComponent<Food>();
             foodComp.entityType = chosenFoodType;
         }
         else
            foodComp = foodObj.GetComponent<Food>();

        return foodObj;
    }
    
}
