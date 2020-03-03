using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "EntityType")]


 /// <summary>
 /// all the entities in the game i.e Food, Hazards, Decorations...,  should at the very least have a name
 /// </summary>

public class EntityType : ScriptableObject {
    public string entityName;

}
