using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public  class DisplayRegularStoreItem : DisplayStoreItem<RegularStoreItem>
{
    public RegularStoreItem _storeItem;

    public override RegularStoreItem StoreItem {
        get{
            return _storeItem;
        }
        set{
            _storeItem = (RegularStoreItem)value;
        }
    }
    protected override void Start()
    {
        base.Start();
        itemName.text = StoreItem.name;
    }
}
