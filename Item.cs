using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public enum ITEMTYPE
    {
        Ammo,
        Medikit,
        Gun,
        Part,
        Bomb,
    }

    [HideInInspector] public ITEMTYPE itemType;
    public Sprite itemSprite;
    public string itemName;
    public int maxItemCount;   
    public int itemCount;
    public int itemWeight;

    public ItemObject linkedItemObject;         // 아이템이 담겨있는 실제 오브젝트.

    public Item()
    {

    }
    public Item(Item copy)
    {
        itemType = copy.itemType;
        itemSprite = copy.itemSprite;
        itemName = copy.itemName;
        maxItemCount = copy.maxItemCount;
        itemWeight = copy.itemWeight;                
    }

    public static bool operator ==(Item a, Item b)
    {
        return (a.itemType == b.itemType) && (a.itemName == b.itemName);
    }
    public static bool operator !=(Item a, Item b)
    {
        return (a.itemType != b.itemType) || (a.itemName != b.itemName);
    }
}

