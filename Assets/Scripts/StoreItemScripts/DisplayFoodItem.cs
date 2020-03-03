using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



/// <summary>
/// responsible for displaying food items
/// </summary>
/// <typeparam name="ComboFood"></typeparam>
public class DisplayFoodItem : DisplayInGameItem<FoodType>
{
    public FoodStoreItem _storeItem;
    public override InGameItem<FoodType> StoreItem
    {
        get { return _storeItem; }
        set { _storeItem = (FoodStoreItem)value; }
    }

    public Text Score;

    protected override void Start()
    {
       // Score.text = StoreItem.entity.scoreValue;
       itemImage.sprite = StoreItem.entity.FoodImg;
       base.Start();
    }
    
    protected override void Buy()
    {
        //GameManager.Instance.UnlockedItems.Add("Food", item);
        Debug.Log("Item has been bought - derived");
        base.Buy();
    }
}

// dummy class so it can be serialized in the editor
[System.Serializable]
public class FoodStoreItem : InGameItem<FoodType> {}
