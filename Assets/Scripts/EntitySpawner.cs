using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Contains spawn information like how many entities to spawn,
/// how many entities are currently in the world,
/// how often they should spawn,
/// and when they spawn how far from the planet should they spawn
/// </summary>

[System.Serializable]

public class EntitySpawnInfo
{
    [Range(0, 10)]
    public float threshold;

    public int maxAmt;
    public int currAmt;
    public float distFromWorld; //spawn distance from world
    public float spawnInterval;

}

/// <summary>
/// The HazardSpawnInfo needs a duration which says how long each hazard 
/// will last before going to the next. For example 1 minute of chocolate volcanos
///  but maybe you want 40 seconds of salt tornados. You can specify that here.
/// this class should actually be in HazardSpawner, but oh well lol, I will move
/// it later.
/// </summary>
/// 
[System.Serializable]
public class HazardSpawnInfo : EntitySpawnInfo
{
    public float duration;
   
}

/// <summary>
/// This is the EntitySpawner abstract class, it contains a blueprint for all
/// spawners to follow and components they should have access to.
/// All spawners should have access to the factory so they can spawn objects
/// they should have spawn information(how much to spawn, how often etc.)
/// They should all have a list of things they're going to be spawning
/// They should all be singletons that you can access globally
/// </summary>
/// <typeparam name="SpawnInfoType">Spawn Info for your spawner</typeparam>
/// <typeparam name="EntityT">The entity type you'll be spawning(i.e Food, Hazard or a base EntityType)</typeparam>
/// 
public abstract class EntitySpawner <Spawner, SpawnInfoType, EntityT> : MonoBehaviour

    where Spawner: EntitySpawner <Spawner, SpawnInfoType, EntityT>
    where SpawnInfoType : EntitySpawnInfo 
    where EntityT : EntityType
{

    public SpawnWorld world; 

    public EntityT[] entityTypeList; //list of entity types, for example FoodSpawner has Food types
    public SpawnInfoType entityInfo; //stores information on different types of entities (i.e FoodInfo, HazardInfo...)
    public EntityFactory factory; //creates entities


    public static Spawner Instance { get; protected set; }

    /// <summary>
    /// All classes that inherit from EntitySpawner should call base.Spawn(), this will add one
    /// to entityInfo.currAmt, which is the amount of that entity that is in the world
    /// because this behavior is common across all spawners, it's in this base class
    /// </summary>
    
    protected virtual GameObject Spawn(EntityType spawningEntity, bool setActive = true, bool notWithinView = false)
    {
        GameObject entityObj = factory.create(spawningEntity);
        world.spawnObject(entityObj, entityInfo.distFromWorld, setActive, notWithinView);

        entityInfo.currAmt++; 
        return entityObj;
    }

    protected abstract void Spawn();


    /// <summary>
    /// after an object(entity) is removed from the world, for example once the player eats/touches it
    /// it should be returned to the object pool, for memory reasons as you know.
    /// </summary>
    /// <param name="entity">the object that should be deleted from the world</param>
    
    public virtual void returnToPool(Entity<EntityT> entity)
    {
        entityInfo.currAmt--; 

        entity.gameObject.SetActive(false);
        entity.gameObject.transform.parent = transform;
    }
}
