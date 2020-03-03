using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An Entity shares MonoBehaviour properties and also an EntityType denoting the type of the object
/// </summary>
/// <typeparam name="EntityT">The type of the Entity i.e FoodType, HazardType</typeparam>
public class Entity<EntityT> : MonoBehaviour

    where EntityT : EntityType
{
    public EntityT entityType;
}
