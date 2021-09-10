using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectManager : Singletone<ItemObjectManager>
{
    Dictionary<string, ItemObject> prefabs = new Dictionary<string, ItemObject>();

    // Start is called before the first frame update
    void Start()
    {
        // 리소스 파일을 기준으로 해당 경로의 모든 에셋을 검색해서 들고온다.
        ItemObject[] allItemPrefabs = Resources.LoadAll<ItemObject>("Item");
        foreach (ItemObject item in allItemPrefabs)
        {
            prefabs.Add(item.HasItem.itemName, item);       // 실제 아이템의 이름으로 구분.
        }
    }

    public ItemObject GetNewItemObject(Item item)
    {
        // 프리팹을 실체화한 뒤 반환.
        ItemObject newItemObject = Instantiate(prefabs[item.itemName]);
        Transform pivot = PlayerController.Instance.transform;

        newItemObject.transform.position = pivot.position + (pivot.forward * 1f);
        newItemObject.HasItem = item;

        return newItemObject;
    }
}
