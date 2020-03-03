using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayComboItem : DisplayInGameItem<ComboType>
{
    public ComboStoreItem _storeItem;
    public override InGameItem<ComboType> StoreItem
    {
        get { return _storeItem; }
        set{ _storeItem = (ComboStoreItem)value; }
        
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        itemImage.sprite = StoreItem.entity.comboImg;
        base.Start();
    }

    protected override void Buy()
    {
        //GameManager.Instance.UnlockedItems.Add("Combo", item);
        Debug.Log("Item has been bought - derived");
        base.Buy();
    }
}

// //dummy class so it can be serialized in the editor
[System.Serializable]
public class ComboStoreItem : InGameItem<ComboType> {}