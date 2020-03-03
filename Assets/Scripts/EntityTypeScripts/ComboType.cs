using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Combo")]
public class ComboType : EntityType {
    
    public int score; //# score gained from completing combo
    public FoodType[] foods; //# list of foods contained in the combo
    public bool isPartial; //# denotes whether the combo is "partial" (a subset of a bigger combo)
    public Sprite comboImg;

    public ComboType(params FoodType[] _foods)
    {
        foods = _foods;
    }
}
