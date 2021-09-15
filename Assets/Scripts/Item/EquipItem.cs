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

    public EquipItem() : base()
    {

    }
    public EquipItem(Dictionary<string, object> data) : base(data)
    {
        equipType = (EQUIPTYPE)System.Enum.Parse(typeof(EQUIPTYPE), data["ItemKind"].ToString());
        itemLevel = int.Parse(data["ItemLevel"].ToString());
    }

    public override Item GetCopy()
    {
        EquipItem newItem = new EquipItem();
        CopyTo(newItem);
        
        newItem.equipType = equipType;
        newItem.itemLevel = itemLevel;

        return newItem;
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
