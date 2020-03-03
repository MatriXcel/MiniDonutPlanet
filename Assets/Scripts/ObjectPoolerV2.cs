using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PooledObject
{
    [Tooltip(@"Name is used to differ the objects from one another")]
    public EntityType entity;

    [Tooltip(@"What object should be created ?")]
    public GameObject obj;

    [Range(1, 10000)]
    [Tooltip(@"How much objects should be created ?")]
    public int amount;

    [Tooltip(@"Can new objects be created in case there are none left ?")]
    public bool canGrow;

    [Tooltip(@"False - objects must be created manually using Populate method
True - objects will be created automatically on awake")]
    public bool createOnAwake;
}


public class ObjectPoolerV2 : MonoBehaviour
{


    public PooledObject[] ObjectsToPool;

    private readonly Dictionary<EntityType, List<GameObject>> pooledObjects =
        new Dictionary<EntityType, List<GameObject>>();


    private readonly Dictionary<EntityType, PooledObject> pooledObjectsContainer =
        new Dictionary<EntityType, PooledObject>();

    private void Awake()
    {
        foreach (PooledObject objectToPool in ObjectsToPool)
        {
            pooledObjects.Add(objectToPool.entity, new List<GameObject>());
            pooledObjectsContainer.Add(objectToPool.entity, objectToPool);


            if (objectToPool.createOnAwake)
            {
                populate(objectToPool.entity);
            }
        }
    }

    public void populate(EntityType entity)
    {
        pooledObjects[entity].Clear();

   
        for (int i = 0; i < pooledObjectsContainer[entity].amount; i++)
        {
            GameObject obj = Instantiate(pooledObjectsContainer[entity].obj);
            obj.SetActive(false);
            pooledObjects[entity].Add(obj);
        }
    }


    public GameObject getPooledObject(EntityType entity)
    {
        for (int i = 0; i < pooledObjects[entity].Count; i++)
        {
            if (!pooledObjects[entity][i].activeInHierarchy)
            {
                Debug.Log("condition reached");
                return pooledObjects[entity][i];
            }
        }
        if (pooledObjectsContainer[entity].canGrow)
        {
            GameObject obj = Instantiate(pooledObjectsContainer[entity].obj);
            pooledObjects[entity].Add(obj);
            return obj;
        }

        return null;
    }

}

