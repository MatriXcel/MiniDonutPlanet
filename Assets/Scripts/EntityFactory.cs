using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class EntityFactory {

    public ObjectPoolerV2 pooler;

    public GameObject create(EntityType type)
    {
        return pooler.getPooledObject(type);
    }

}
