using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equip", menuName = "Create Item/New Item/Equipmenet")]
public class EquipItem : Item
{
    [SerializeField] EQUIPTYPE equipType;
    [SerializeField] int itemLevel;

    public EQUIPTYPE Type => equipType;
    public int ItemLevel => itemLevel;

    public enum EQUIPTYPE
    {
        Helmet,
        Bag,
        Armor,
        UtilityBelt,
    }

    public override Item GetCopy()
    {
        EquipItem copy = new EquipItem();

        // Item
        copy.itemType = ITEMTYPE.Equipment;
        copy.itemSprite = itemSprite;
        copy.itemName = itemName;
        copy.maxItemCount = maxItemCount;
        copy.itemCount = itemCount;
        copy.itemWeight = itemWeight;

        // EquipItem
        copy.equipType = equipType;

        return copy;
    }

    public override bool EqualsItem(EQUIPTYPE equipType)
    {
        return this.equipType == equipType;
    }
    public override bool EqualsItem(Item other)
    {
        return base.EqualsItem(other) && other.EqualsItem(equipType);
    }
    public override EquipItem ConvertToEquip()
    {
        return this;
    }
}
