using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//[CreateAssetMenu(fileName = "New Ammo", menuName = "Create Item/New Item/Ammo")]
public class AmmoItem : Item
{
    [SerializeField] AMMOTYPE ammoType;

    public enum AMMOTYPE
    {
        Ammo_5_56mm,
        Ammo_7_62mm,
        Ammo_4_5mm,
    }

    public AmmoItem() : base()
    {

    }
    public AmmoItem(Dictionary<string, object> data) : base(data)
    {
        ammoType = (AMMOTYPE)System.Enum.Parse(typeof(AMMOTYPE), data["ItemKind"].ToString());
    }
    public override Item GetCopy()
    {
        AmmoItem copy = new AmmoItem();         // 새로운 객체 생성.
        CopyTo(copy);                           // 해당 객체에 내가 상속하고 있는 Item 내부 변수 값 복사.

        copy.ammoType = ammoType;               // AmmoItem의 정보 복사.

        return copy;
    }

    public override bool EqualsItem(AMMOTYPE ammoType)
    {
        return this.ammoType == ammoType;
    }
    public override bool EqualsItem(Item other)
    {
        return base.EqualsItem(other) && other.EqualsItem(ammoType);
    }

    
}

