using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ammo", menuName = "Create Item/New Item/Ammo")]
public class AmmoItem : Item
{
    [SerializeField] AMMOTYPE ammoType;

    public enum AMMOTYPE
    {
        Ammo_5_56mm,
        Ammo_7_62mm,
    }

    public override bool EqualsItem(AMMOTYPE ammoType)
    {
        return this.ammoType == ammoType;
    }
    public override bool EqualsItem(Item other)
    {
        return base.EqualsItem(other) && other.EqualsItem(ammoType);
    }

    public override Item GetCopy()
    {
        AmmoItem copy = new AmmoItem();
        // Item
        copy.itemType = ITEMTYPE.Ammo;
        copy.itemSprite = itemSprite;
        copy.itemName = itemName;
        copy.maxItemCount = maxItemCount;
        copy.itemCount = itemCount;
        copy.itemWeight = itemWeight;

        // AmmoItem
        copy.ammoType = ammoType;

        return copy;
    }
}

