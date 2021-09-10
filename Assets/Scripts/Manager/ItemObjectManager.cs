using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectManager : Singletone<ItemObjectManager>
{
    Dictionary<string, ItemObject> prefabs = new Dictionary<string, ItemObject>();

    // Start is called before the first frame update
    void Start()
    {
        // ���ҽ� ������ �������� �ش� ����� ��� ������ �˻��ؼ� ���´�.
        ItemObject[] allItemPrefabs = Resources.LoadAll<ItemObject>("Item");
        foreach (ItemObject item in allItemPrefabs)
        {
            prefabs.Add(item.HasItem.itemName, item);       // ���� �������� �̸����� ����.
        }
    }

    public ItemObject GetNewItemObject(Item item)
    {
        // �������� ��üȭ�� �� ��ȯ.
        ItemObject newItemObject = Instantiate(prefabs[item.itemName]);
        Transform pivot = PlayerController.Instance.transform;

        newItemObject.transform.position = pivot.position + (pivot.forward * 1f);
        newItemObject.HasItem = item;

        return newItemObject;
    }
}
