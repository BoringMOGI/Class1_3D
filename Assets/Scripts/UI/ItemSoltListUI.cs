using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSoltListUI : PoolManager<ItemSoltListUI, BasicSlotUI>
{
    [SerializeField] RectTransform uiTransform;         // 나 자신의 Rect.
    [SerializeField] Transform itemListParent;          // itemList이 부모 오브젝트.

    public RectTransform Rect => uiTransform;

    public void SetItemList(List<Item> itemList)
    {
        Clear();
        for (int i = 0; i < itemList.Count; i++)
        {
            BasicSlotUI pool = GetPool();
            pool.SetSlot(itemList[i]);
            pool.transform.SetParent(itemListParent);
        }
    }
}
