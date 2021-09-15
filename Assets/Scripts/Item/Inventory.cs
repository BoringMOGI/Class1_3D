using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singletone<Inventory>
{
    List<Item> itemList = new List<Item>();   // 아이템 리스트.
    float currentWeight;                      // 가방의 무게.
    float maxWeight;                          // 가방의 최대 무게.    

    public event System.Action OnUpdateInventory;  // 인벤토리에 변화가 생김.
    PlayerController player;

    public int IndexCount => itemList.Count;       // 아이템의 개수.
    public float Weight => currentWeight;
    public float MaxWeight => maxWeight;

    private void Start()
    {
        maxWeight = 100f;
        player = PlayerController.Instance;

        UpdatedInventory();
    }

    public void PutItem(Item putItem)
    {
        Item newItem = putItem.GetCopy();

        // 남은 무게가 허용하는 만큼
        int maxAllowedCount = (int)((maxWeight - currentWeight) / putItem.itemWeight);

        // 아이템의 개수가 허용치 안이라면.
        if (putItem.itemCount <= maxAllowedCount)
        {
            putItem.itemCount = 0;
        }
        else
        {
            newItem.itemCount = maxAllowedCount;
            putItem.itemCount -= maxAllowedCount;
        }

        // 기존에 동일한 아이템이 있다면 병합한다.
        foreach (Item item in itemList)
        {
            if ((item != newItem) || item.IsFull)
                continue;

            // 새로 넣으려는 아이템과 같은 아이템이 존재한다.
            // 해당 아이템은 아직 여유 공간이 있다.
            int totalCount   = item.itemCount + newItem.itemCount;
            int newItemCount = Mathf.Clamp(totalCount - item.maxItemCount, 0, item.maxItemCount);
            int itemCount    = totalCount - newItemCount;

            item.itemCount = itemCount;
            newItem.itemCount = newItemCount;

            if (newItem.itemCount <= 0)
                continue;
        }

        if(newItem.itemCount > 0)
            itemList.Add(newItem);
               
        UpdatedInventory();
    }
    public void RemoveItem(int index)
    {
        if (index < 0 || itemList.Count <= index)
        {
            Debug.Log(string.Format("[RemoveItem] {0}번째는 인벤토리의 범위를 벗어납니다.", index));
            return;
        }

        // 리스트에서 제거.
        Item removedItem = itemList[index];              // 제거할 아이템 정보 대입.
        itemList.RemoveAt(index);                        // 리스트에서 아이템 제거.

        // 아이템 생성.
        ItemManager.Instance.ConvertToObject(removedItem, PlayerController.Instance.RemoveItemPosition());

        UpdatedInventory();
    }

    
    public void InvenToEquip(int index)                 // 인벤토리에서 장비 슬롯으로 이동.
    {
        Item requestItem = itemList[index];
        if (requestItem.itemType != Item.ITEMTYPE.Equipment)
            return;

        itemList.RemoveAt(index);
        requestItem = player.OnEquipItem(requestItem);  // 장비 장착 시도(장비X, 이미 장비한 게 있어서 return되거나)

        if(requestItem != null)
            itemList.Add(requestItem);

        UpdatedInventory();
    }

    public bool IsEnougthItem(AmmoItem.AMMOTYPE ammoType, int count)
    {
        foreach(Item item in itemList)
        {
            if (item.EqualsItem(ammoType) && item.itemCount >= count)
                return true;
        }

        return false;
    }
    public int GetItem(AmmoItem.AMMOTYPE ammoType, int needCount)
    {
        int getCount = 0;                       // 내보내는 개수.

        // needCount만큼 요청했으나 needCount이하 값을 줄 수 있다.
        for(int i = 0; i<itemList.Count;)
        {
            if (needCount <= 0)
                break;

            Item item = itemList[i];
            if (item.EqualsItem(ammoType) == false)
            {
                i++;
                continue;
            }

            int itemCount = item.itemCount;
            if(itemCount <= needCount)              // (불충분)해당 아이템의 개수가 원하는 개수보다 적다.
            {
                getCount += itemCount;              // 해당 아이템의 모든 개수를 추가한다.
                needCount -= itemCount;             // 필요 카운트를 아이템 개수만큼 감소.
                itemList.RemoveAt(i);               // i번째 아이템의 개수가 0개가 되어 리스트에서 삭제.
            }
            else                                    // 아이템의 개수가 원하는 개수 만큼 충분하다.
            {
                itemList[i].itemCount -= needCount; // 아이템의 개수를 원하는 개수 만큼 감소.
                getCount += needCount;              // 원하는 개수만큼 getCount에 추가.
                needCount = 0;                      // 원하는 개수는 0.
                i++;
            }
        }

        UpdatedInventory();

        return getCount;
    }
    public int ItemCount(AmmoItem.AMMOTYPE ammoType)
    {
        int itemCount = 0;

        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].EqualsItem(ammoType))
                itemCount += itemList[i].itemCount;
        }

        return itemCount;
    }

    private void UpdatedInventory()
    {
        // 무게 계산.
        currentWeight = 0f;
        for (int i = 0; i < itemList.Count; i++)
            currentWeight += itemList[i].itemWeight * itemList[i].itemCount;

        if (InventoryUI.Instance == null)
            return;

        InventoryUI.Instance.SetInventory(itemList, currentWeight / maxWeight);     // UI 갱신.
        OnUpdateInventory?.Invoke();
    }
}
