using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemSlotUI : BasicSlotUI
{
    [SerializeField] Text nameText;
    [SerializeField] Text countText;

    public override void SetSlot(Item item)
    {
        base.SetSlot(item);

        nameText.text    = item.itemName;
        countText.text   = item.itemCount.ToString();
    }
}
