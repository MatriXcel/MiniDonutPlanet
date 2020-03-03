using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public abstract class Section<EntityT> where EntityT : StoreItem
{

    public abstract List<EntityT> Items {get;}
    public GameObject container;
    public GameObject template;
    
    public void loadSection()
    {
        for(int i = 0; i < Items.Count; i++)
        {
             GameObject _template = GameObject.Instantiate(template);
             _template.GetComponent<DisplayStoreItem<EntityT>>().StoreItem = Items[i];

             _template.transform.SetParent(container.transform);
             _template.transform.localScale = container.transform.localScale;
             
        }
    }
};

//created a dummy class because unity can't serialize generics in this case, there's no other way around this
[System.Serializable]
public class FoodSection : Section<InGameItem<FoodType>> 
{
    public List<FoodStoreItem> _items;
    public override List<InGameItem<FoodType>> Items 
    {
         get { return _items.Cast<InGameItem<FoodType>>().ToList(); }
    }
}

[System.Serializable]
public class ComboSection : Section<InGameItem<ComboType>> 
{
    public List<ComboStoreItem> _items;
    public override List<InGameItem<ComboType>> Items 
    {
         get { return _items.Cast<InGameItem<ComboType>>().ToList(); }
    }
}


public class StoreManager : MonoBehaviour
{
    public FoodSection foodSection;
    // public ComboSection comboSection;

    void Start()
    {
        foodSection.loadSection();
        // comboSection.loadSection();
    }
}

