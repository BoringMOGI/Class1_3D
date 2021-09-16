using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equip", menuName = "Create Item/New Item/Equipmenet")]
public class EquipItem : Item
{
    [SerializeField] EQUIPTYPE equipType;

    public EQUIPTYPE Type => equipType;
    
    public enum EQUIPTYPE
    {
        Helmet_1,
        Helmet_2,
        Helmet_3,
        Backpack_1,
        Backpack_2,
        Backpack_3,
        Armor_1,
        Armor_2,
        Armor_3,
        UtilityBelt,
    }

    public EquipItem() : base()
    {

    }
    public EquipItem(Dictionary<string, object> data) : base(data)
    {
        equipType = (EQUIPTYPE)System.Enum.Parse(typeof(EQUIPTYPE), data["ItemKind"].ToString());
    }

    public override Item GetCopy()
    {
        EquipItem newItem = new EquipItem();
        CopyTo(newItem);
        
        newItem.equipType = equipType;

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
