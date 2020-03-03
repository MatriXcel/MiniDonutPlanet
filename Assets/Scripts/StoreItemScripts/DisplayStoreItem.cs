using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
/// <summary>
/// stores information about the StoreItem
/// </summary>
/// <typeparam name="EntityT"></typeparam>
public abstract class StoreItem
{
    public float price;
    private bool isUnlocked;

    public void Unlock()
    {
        isUnlocked = true;
    }
    
}

public class RegularStoreItem : StoreItem
{
    public string name;
}

public class InGameItem<EntityT>  : StoreItem
where EntityT : EntityType

{
    public EntityT entity;

}



/// <summary>
/// every DisplayItem class inherits from this base, this base defines properties every
/// DisplayItem class should have
/// </summary>
/// <typeparam name="StoreItemT">i.e Food, Combo, Powerup</typeparam>
public abstract class DisplayStoreItem<StoreItemT> : MonoBehaviour
 where StoreItemT : StoreItem 
{
    public abstract StoreItemT StoreItem {get; set;}
    
    public Text itemName;
    public Text price;
    public Button buyButton;
    public Image itemImage;

    protected virtual void Start()
    {
        buyButton.onClick.AddListener(Buy);
        price.text = StoreItem.price.ToString("C2");
    }

    protected virtual void Buy()
    {
        Debug.Log("Item has been bought - base");
        StoreItem.Unlock();
        //change color of box here
    }
}



public abstract class DisplayInGameItem<EntityT> : DisplayStoreItem<InGameItem<EntityT>>
    where EntityT : EntityType
{
    protected override void Start()
    {
        base.Start();
        itemName.text = StoreItem.entity.entityName;
    }
}