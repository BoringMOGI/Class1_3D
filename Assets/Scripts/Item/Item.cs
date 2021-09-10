using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public enum ITEMTYPE
    {
        Ammo,
        Equipment,
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

    public abstract Item GetCopy();


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

