using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryUI : Singletone<InventoryUI>
{
    public enum AREA_TYPE
    {
        None,
        GroundUI,
        InventoryUI,
        EquipUI,
    }

    [Header("Ground")]
    [SerializeField] ItemSoltListUI groundItemUI;       // 바닥 아이템 리스트 UI.
    [SerializeField] RectTransform groundItemArea;      // 바닥 아이템 UI 영역.

    [Header("Inventory")]
    [SerializeField] ItemSoltListUI inventoryItemUI;    // 인벤토리 리스트 UI.
    [SerializeField] RectTransform inventoryUIArea;     // 인벤토리 UI 영역.

    [Header("Equip")]
    [SerializeField] EquipSlotListUI qeuipItemUI;
    [SerializeField] RectTransform equipUIArea;

    [Header("Etc")]
    [SerializeField] BasicSlotUI previewItem;            // 미리보기 아이템.
    [SerializeField] Image weightImage;                 // 무게 이미지.

    public bool IsOpenInventory => gameObject.activeSelf;

    private void Start()                    // 게임 오브젝트가 켜지는 순간(1회)
    {
        gameObject.SetActive(false);
        ShortcutManager.Instance.RegestedShutcut(OnSwitchInventory);
    }
    private void OnEnable()                 // 게임 오브젝트가 켜지는 순간(반복)
    {
        Cursor.lockState = CursorLockMode.None;
    }
    private void OnDisable()                // 게임 오브젝트가 꺼지는 순간(반복)
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnDestroy()
    {
        ShortcutManager.Instance.RemoveShortcut(OnSwitchInventory);
    }

    
    // 인벤토리 제어.
    public void SetInventory(List<Item> itemList, float weightAmount)
    {
        inventoryItemUI.SetItemList(itemList);
        weightImage.fillAmount = weightAmount;
    }
    public void SetGroundItem(List<Item> itemList)
    {
        groundItemUI.SetItemList(itemList);
    }
    public void OnSwitchInventory(KeyCode key)
    {
        if (key != KeyCode.Tab)
            return;

        gameObject.SetActive(!gameObject.activeSelf);
    }

    public AREA_TYPE GetCurrentArea()       // 호출되는 순간 마우스 포인터가 있는 영역.
    {
        AREA_TYPE area = AREA_TYPE.None;
        if (RectTransformUtility.RectangleContainsScreenPoint(groundItemArea, Input.mousePosition))
        {
            area = AREA_TYPE.GroundUI;
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(inventoryUIArea, Input.mousePosition))
        {
            area = AREA_TYPE.InventoryUI;
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(equipUIArea, Input.mousePosition))
        {
            area = AREA_TYPE.EquipUI;
        }

        return area;
    }


    // 이벤트 함수.
    public void OnBeginDragPreview(Item item)
    {
        previewItem.gameObject.SetActive(true);
        previewItem.SetSlot(item);
    }
    public void OnDraggingPreview()
    {
        previewItem.transform.position = Input.mousePosition;
    }
    public void OnEndDragPreview()
    {
        previewItem.gameObject.SetActive(false);
    }
}
