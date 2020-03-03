using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// responsible for spawning decorations
/// </summary>
/// <typeparam name="EntitySpawnInfo">some spawn information on how decorations should spawn
/// i.e How often, how much, how far from the planet to spawn
/// </typeparam>
/// <typeparam name="EntityType">contains information that is specific to a decoration</typeparam>
/// 
public class DecorationSpawner : EntitySpawner <DecorationSpawner, EntitySpawnInfo, EntityType> {

	// Use this for initialization


	void Awake () {
        Instance = this;
	}

    void Update()
    {
        //as long as we haven't hit the max amount of decorations we can spawn
        //keep spawning
        if(entityInfo.currAmt < entityInfo.maxAmt)
        {
            Spawn();
        }
    }

    protected override void Spawn()
    {
        base.Spawn(entityTypeList[Random.Range(0, entityTypeList.Length)], notWithinView: true);
    }


}
