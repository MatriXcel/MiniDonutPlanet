using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for spawning Hazards
/// </summary>
/// <typeparam name="HazardSpawnInfo">contains the same kind of info as the FoodSpawner
/// but each hazard also has a duration
/// </typeparam>
/// <typeparam name="EntityType">
/// The hazards you will be spawning
/// </typeparam>

[System.Serializable]

public class HazardSpawner : EntitySpawner <HazardSpawner, HazardSpawnInfo, EntityType> {

    float currentTime, targetTime, maxInterval;
    int pickedHazardIndex; 
    
    /*an array of spawn information about each hazard, each hazard
      corresponds to a HazardSpawnInfo, for example volcano will corresponds to
      VolcanoSpawnInfo. This should actually be a Dictionary lol. But i got lazy
      so just make sure the position of the hazard in the entities list is the same
      as the position of its corresponding Hazard in the hazardInfos list.
     */

    public HazardSpawnInfo[] hazardInfos; 



	void Awake () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {

        //if it's time to spawn a new hazard and there's no more objects of the past hazard
        if (currentTime >= targetTime && entityInfo.currAmt == 0)
        {
            //pick the spawn info of a random hazard
            pickedHazardIndex = Random.Range(0, entityTypeList.Length);
            entityInfo = hazardInfos[pickedHazardIndex];

            //update the next time we spawn a different hazard
            targetTime = Time.time + entityInfo.duration;
        }

        else
        {
            if(currentTime >= maxInterval)
            {
                maxInterval = currentTime + entityInfo.spawnInterval;

                //pick a random number between 0 and 10
                float test = Random.Range(0, 10);

                //if it meets threshold and we haven't hit the limit of the amount that can exist in the world, then spawn
                if (test < entityInfo.threshold && entityInfo.currAmt < entityInfo.maxAmt)
                {
                    Spawn();
                }
            }
        }

       
        currentTime += Time.deltaTime;
    }

    //gets the hazard from the factory(the pool) and spawns it into the world
    protected override void Spawn()
    {
        base.Spawn(entityTypeList[pickedHazardIndex]);
    }

}
