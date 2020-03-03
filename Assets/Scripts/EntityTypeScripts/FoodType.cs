using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Food")]

/// <summary>
/// A FoodType has the properties of a scoreValue and a Food image
/// </summary>
public class FoodType : EntityType
{
    public int scoreValue;
    public Sprite FoodImg; //the image of the food to be used on the plates

}
