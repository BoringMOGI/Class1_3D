using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Globalization;

public class ItemManager : MonoBehaviour
{
    [SerializeField] TextAsset text;
    [SerializeField] List<Item> itemList;

    RandomBox<Item> randomBox;

    [ContextMenu("Read CSV File")]
    public void ReadCsvFile()
    {
        itemList = new List<Item>();

        List<Dictionary<string, object>> data = CSVReader.Read("ItemData");
        for (int i = 0; i < data.Count; i++)
            CreateItem(data[i]);

        randomBox = new RandomBox<Item>();
        for(int i = 0; i<itemList.Count; i++)
            randomBox.Push(itemList[i], 10);
    }

    private void CreateItem(Dictionary<string, object> data)
    {
        string itemName = data["Name"] as string;
        Item.ITEMTYPE itemType = (Item.ITEMTYPE)System.Enum.Parse(typeof(Item.ITEMTYPE), data["ItemType"] as string);
        int itemCount = (int)data["ItemCount"];
        int maxItemCount = (int)data["MaxCount"];
        float weight = (float)data["Weight"];
        Sprite itemSprite = Resources.Load<Sprite>(string.Concat("ItemSprite/", itemName));
        ItemObject itemObject = Resources.Load<ItemObject>(string.Concat("ItemObject/", itemName));

        Item item = null;

        switch(itemType)
        {
            case Item.ITEMTYPE.Ammo:
                item = new AmmoItem();
                item.itemName = itemName;
                item.itemType = itemType;
                item.itemCount = itemCount;
                item.maxItemCount = maxItemCount;
                item.itemWeight = weight;
                item.itemSprite = itemSprite;
                item.itemObject = itemObject;
                break;

            case Item.ITEMTYPE.Equipment:
                break;
        }

        itemList.Add(item);
    }

    public Item GetItem(AmmoItem.AMMOTYPE ammoType)
    {
        for(int i = 0; i<itemList.Count; i++)
        {
            if (itemList[i].EqualsItem(ammoType))
                return itemList[i].GetCopy();
        }

        return null;
    }
    public Item GetItem(EquipItem.EQUIPTYPE equipType)
    {
        return null;
    }
    public Item GetRandomItem()
    {
        return randomBox.Pick();
    }
    
}
