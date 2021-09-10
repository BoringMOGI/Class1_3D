using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static InventoryUI;

public class BasicSlotUI : PoolObject<BasicSlotUI>
{
    [SerializeField] protected Image iconImage;

    InventoryUI invenUI;   // 인벤토리 UI 매니저.
    Item hasItem;          // 가지고 있는 아이템 정보. 
    AREA_TYPE beforeArea;  // 이전 Area 종류.

    public virtual void SetSlot(Item item)
    {   
        hasItem = item;
        invenUI = InventoryUI.Instance;

        if (item != null)
        {
            iconImage.sprite = item.itemSprite;
            iconImage.enabled = true;
        }
        else
            iconImage.enabled = false;
    }

    public void OnBeginDrag()
    {
        if (hasItem == null)
            return;

        invenUI.OnBeginDragPreview(hasItem);
        beforeArea = invenUI.GetCurrentArea();
    }
    public void OnDragging()
    {
        if (hasItem == null)
            return;

        invenUI.OnDraggingPreview();
    }
    public void OnEndDrag()
    {
        if (hasItem == null)
            return;

        invenUI.OnEndDragPreview();

        AREA_TYPE currentArea = invenUI.GetCurrentArea();

        if (currentArea == AREA_TYPE.None)              // 현재 위치가 아무 영역도 아니라면.
            return;

        if (currentArea == beforeArea)                  // 이전 영역과 현재 영역이 같을 경우.
            return;


        int itemIndex = transform.GetSiblingIndex();    // 내가 몇번째 자식인지?

        switch(currentArea)
        {
            case AREA_TYPE.GroundUI:                                    // 도착 지점이 그라운드 UI.
                if (beforeArea == AREA_TYPE.EquipUI)                    // 장비창 -> 땅바닥
                {
                    if(PlayerController.Instance.OnEquipToGround(hasItem))
                        SetSlot(null);
                }
                else
                {
                    Inventory.Instance.RemoveItem(itemIndex);
                }
                break;
            case AREA_TYPE.InventoryUI:                                 // 도착 지점이 인벤토리 UI.
                if (beforeArea == AREA_TYPE.EquipUI)                    // 장비창 -> 인벤토리.
                {
                    if (PlayerController.Instance.OnEquipToInven(hasItem))
                        SetSlot(null);
                }
                else
                {
                    GroundItemFinder.Instance.OnGroundToInven(itemIndex);
                }
                break;
            case AREA_TYPE.EquipUI:                                     // 도착 지점이 장비 UI.

                if(beforeArea == AREA_TYPE.GroundUI)                    
                {
                    GroundItemFinder.Instance.OnGroundToEquip(itemIndex);
                }
                else if(beforeArea == AREA_TYPE.InventoryUI)
                {
                    Inventory.Instance.InvenToEquip(itemIndex);
                }
                
                break;
        }
    }
}
