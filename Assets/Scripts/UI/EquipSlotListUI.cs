using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EquipItem;

public class EquipSlotListUI : Singletone<EquipSlotListUI>
{
    [System.Serializable]
    public class EquipSlot
    {
        public EquipSlotUI slotUI;
        public EquipItem.EQUIPTYPE type;
    }

    [SerializeField] EquipSlot[] equipSlots;

    EquipSlotUI GetSlotUI(EQUIPTYPE type)
    {
        for(int i = 0; i<equipSlots.Length; i++)
        {
            if (equipSlots[i].type == type)     // i번째 List의 type이 같다면
                return equipSlots[i].slotUI;    // 해당 아이템을 리턴한다.
        }

        return null;
    }

    public void SetEquipItem(EquipItem equipItem)           // 해당 아이템에 맞는 곳에 넣어라.
    {
        for(int i = 0; i< equipSlots.Length; i++)
        {
            if(equipSlots[i].type == equipItem.Type)        // i번째 슬롯의 Type이 동일한 경우.
            {
                equipSlots[i].slotUI.SetSlot(equipItem);    // 해당 슬롯에 아이템 전달.
                break;
            }
        }
    }
}
