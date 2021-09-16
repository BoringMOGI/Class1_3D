using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Item
{
    public enum ITEMTYPE
    {
        Ammo,
        Equipment,
        Weapon,
    }

    [HideInInspector]
    public ITEMTYPE itemType;
    public Sprite itemSprite;
    public string itemName;
    public int maxItemCount;   
    public int itemCount;
    public float itemWeight;

    public bool IsFull => (itemCount >= maxItemCount);

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    public override string ToString()
    {
        return string.Format("{0}({1})", itemName, itemCount);
    }

    protected Item()
    {

    }
    protected Item(Dictionary<string, object> data)
    {
        itemType = (Item.ITEMTYPE)System.Enum.Parse(typeof(Item.ITEMTYPE), data["ItemType"] as string);
        itemName = data["Name"] as string;
        itemCount = int.Parse(data["ItemCount"].ToString());
        maxItemCount = int.Parse(data["MaxCount"].ToString());
        itemWeight = float.Parse(data["Weight"].ToString());

        itemSprite = Resources.Load<Sprite>(string.Concat("ItemSprite/", data["SpriteName"].ToString()));
    }

    public abstract Item GetCopy();
    protected void CopyTo(Item target)
    {
        target.itemType = itemType;
        target.itemName = itemName;
        target.itemCount = itemCount;
        target.maxItemCount = maxItemCount;
        target.itemWeight = itemWeight;
        target.itemSprite = itemSprite;
    }
    
    

    public virtual bool EqualsItem(AmmoItem.AMMOTYPE ammoType)
    {
        return false;
    }
    public virtual bool EqualsItem(EquipItem.EQUIPTYPE equipType)
    {
        return false;
    }
    public virtual bool EqualsItem(Item other)
    {
        return itemType == other.itemType;
    }

    public virtual EquipItem ConvertToEquip()
    {
        return null;
    }
}

