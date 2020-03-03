using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DisplayCombo : MonoBehaviour {

	
	void Start () {
        //subscribe to the ComboManager's onComboUpdate() event
        ComboManager.Instance.onComboUpdate += onUpdate;
	}


    void onUpdate(ComboType _currentCombo)
    {
        for (int i = 0; i < 4; i++)
        {
            Image currImage = transform.GetChild(i).GetChild(0).GetComponent<Image>();

           
            if (i >= _currentCombo.foods.Length)
            {
                currImage.color = Color.clear;
                currImage.sprite = null;

                continue;
            }

            Sprite newSprite = _currentCombo.foods[i].FoodImg;
            currImage.color = Color.white;
            currImage.sprite = newSprite;
        }
    }
   
}
