using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItemFinder : Singletone<GroundItemFinder>
{
    List<ItemObject> groundItemList = new List<ItemObject>();       // 전체 검색.
    ItemObject groundItemOfForword = null;                              // Raycast 검색.

    [Header("Eye")]
    [SerializeField] Transform eyePivot;          // 아이템 탐색 기준점(눈)
    [SerializeField] float eyeDistance;           // 아이템 탐색 길이(눈)

    [Header("Ground")]
    [SerializeField] Transform searchItemPivot;   // 아이템 탐색 기준점.
    [SerializeField] float searchRadius;          // 아이템 탐색 범위.
    [SerializeField] LayerMask itemMask;          // 아이템 마스크.

    public ItemObject FirstGroundItem             // 첫번째 탐색 아이템.
    {
        get
        {
            if (groundItemOfForword != null)
                return groundItemOfForword;
            else if (groundItemList.Count <= 0)
                return null;
            else
                return groundItemList[0];
        }
    }

    private void Start()
    {
        
    }
    private void Update()
    {
        UpdateGroundItem();
    }

    void UpdateGroundItem()
    {
        // Raycast로 하나만 검색.
        RaycastHit rayHit;
        if (Physics.Raycast(eyePivot.position, eyePivot.forward, out rayHit, eyeDistance, itemMask))
            groundItemOfForword = rayHit.collider.GetComponent<ItemObject>();
        else
            groundItemOfForword = null;


        // ShereCast로 전체 검색.
        RaycastHit[] hits = Physics.SphereCastAll(searchItemPivot.position, searchRadius, Vector3.down, 2f, itemMask);
        groundItemList.Clear();

        List<Item> itemList = new List<Item>();

        // 바닥을 탐색한다.
        foreach (RaycastHit hit in hits)
        {
            ItemObject groundItem = hit.collider.GetComponent<ItemObject>();
            if (groundItem != null)
            {
                groundItemList.Add(groundItem);                 // 아이템 오브젝트의 리스트.
                itemList.Add(groundItem.HasItem);               // 아이템의 리스트.
            }
        }

        if(InventoryUI.Instance != null)
            InventoryUI.Instance.SetGroundItem(itemList);           // 아이템의 리스트를 인벤토리에 전달.
    }

    public void OnGroundToInven(int index)
    {
        ItemObject itemObject = null;
        if (index < 0)
            itemObject = FirstGroundItem;
        else
            itemObject = groundItemList[index];

        Item putItem = itemObject.HasItem;                      // ItemObject가 가지고 있는 Item.
        Inventory.Instance.PutItem(putItem);                    // 인벤토리에 대입.

        if(putItem.itemCount <= 0)
            Destroy(itemObject.gameObject);
        else
            itemObject.HasItem = putItem;
    }
    public void OnGroundToEquip(int index)
    {
        ItemObject itemObject = null;
        if (index < 0)
            itemObject = FirstGroundItem;
        else
            itemObject = groundItemList[index];

        // 해당 아이템이 장비인가?
        if (itemObject.HasItem.itemType != Item.ITEMTYPE.Equipment)
            return;

        Vector3 objectPos = itemObject.transform.position;      // 기존 아이템 오브젝트의 위치.
        Item requestItem = itemObject.HasItem;                  // 아이템 오브젝트에서 아이템 데이터 추출.
        Destroy(itemObject.gameObject);                         // 아이템 오브젝트 삭제.

        requestItem = PlayerController.Instance.OnEquipItem(requestItem);    // 장착 시도.
        if (requestItem != null)                                             // 장비 교체.
        {
            ItemManager.Instance.ConvertToObject(requestItem, objectPos);    // 기존에 장비하던 장비 아이템 바닥에 버림.
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(eyePivot != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(eyePivot.position, eyePivot.forward * eyeDistance);
        }
    }
}
