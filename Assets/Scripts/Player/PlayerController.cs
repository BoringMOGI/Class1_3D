using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : Controller
{
    static PlayerController instance;
    public static PlayerController Instance => instance;

    [SerializeField] Animator anim;               // 애니메이션.
    [SerializeField] Transform aimPivot;          // 에임시 위치.
    [SerializeField] Inventory inventory;         // 인벤토리.
    [SerializeField] Stateable stat;              // 스텟.

    [Header("Interaction")]
    [SerializeField] KeyCode interactionKey;      // 상호작용 키.
    [SerializeField] Transform searchItemPivot;   // 아이템 탐색 기준점.
    [SerializeField] float searchRadius;          // 아이템 탐색 범위.
    [SerializeField] LayerMask itemMask;          // 아이템 마스크.

    Camera mainCam;
    InventoryUI invenUI;

    // 장비 아이템.
    Dictionary<EquipItem.EQUIPTYPE, Item> equipList = new Dictionary<EquipItem.EQUIPTYPE, Item>();      

    bool isAim;                     // 에임 상태인가.
    bool isAlive;                   // 생존 여부.
    float normalFov;                // 기존 FOV.
    float aimFov;                   // 에임시 FOV.

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        mainCam = Camera.main;

        normalFov = mainCam.fieldOfView;
        aimFov = normalFov - 10;
        invenUI = InventoryUI.Instance;

        isAlive = true;

        // dictionary를 EQUIPTYPE에 맞춰서 미리 생성.
        foreach(EquipItem.EQUIPTYPE type in System.Enum.GetValues(typeof(EquipItem.EQUIPTYPE)))
        {
            equipList.Add(type, null);
        }
    }

    private void Update()
    {
        if (isAlive)
        {
            WeaponControl();

            if(invenUI != null && invenUI.IsOpenInventory == false) 
                UpdateSearchItem();     // 인베토리가 열려있지 않을 때.
        }
    }

    // ==================== weapon ===================
    void Reload()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            mainGun.Reload();
        }
    }
    void Aim()
    {
        // 에임.
        isAim = Input.GetMouseButton(1);
        anim.SetBool("isAim", isAim);

        // 카메라의 위치.
        Vector3 camPos = isAim ? aimPivot.position : eyePivot.position;
        float fov = isAim ? aimFov : normalFov;

        mainCam.transform.position = Vector3.MoveTowards(mainCam.transform.position, camPos, 15f * Time.deltaTime);
        mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, fov, 10f * Time.deltaTime);
    }
    void WeaponControl()
    {
        if (InventoryUI.Instance != null && InventoryUI.Instance.IsOpenInventory)       // 인벤토리가 열려있으면 리턴.
            return;

        Reload();
        Aim();

        // 발사.
        if (Input.GetMouseButton(0))
            Fire();
    }

    void UpdateSearchItem()
    {
        ItemObject itemObject = GroundItemFinder.Instance.FirstGroundItem;   // hit된 오브젝트가 ItemObject인가?
        if (itemObject != null)
        {
            // F키 누르면 먹는다.
            InteractionUI.Instance.SetInteractionUI(interactionKey, itemObject.HasItem);
            if (Input.GetKeyDown(interactionKey) == false)
                return;

            Item putItem = itemObject.HasItem;           // 아이템 오브젝트 내부의 데이터 대입.

            if (putItem.itemType == Item.ITEMTYPE.Equipment)
            {
                GroundItemFinder.Instance.OnGroundToEquip(-1);  // 해당 아이템을 장비해라.
            }
            else
            {
                GroundItemFinder.Instance.OnGroundToInven(-1);  // 해당 아이템을 인벤토리에 넣어라.
            }
        }
        else
        {
            InteractionUI.Instance.CloseUI();
        }
    }
    

    public Item OnEquipItem(Item item)
    {
        if (item.itemType != Item.ITEMTYPE.Equipment)
            return item;

        EquipItem equipItem = item.ConvertToEquip();        // 장비하려는 Item을 EquipItem으로 컨버트.
        if (equipItem == null)
            return item;

        Item beforeItem = equipList[equipItem.Type];        // 기존의 장착 장비를 beforeItem으로 대입. (null일 수 있다.)
        equipList[equipItem.Type] = equipItem;
        
        EquipSlotListUI.Instance.SetEquipItem(equipItem);   // UI에 장비하는 아이템 전달.

        return beforeItem;
    }
    public bool OnEquipToInven(Item item)
    {
        EquipItem equip = item.ConvertToEquip();
        if (equip == null)
            return false;

        Item before = equipList[equip.Type];                   // 장비 아이템을 꺼낸다.
        inventory.PutItem(before);                             // 인벤토리에 넣는다.
        equipList[equip.Type] = null;
        return true;
    }
    public bool OnEquipToGround(Item item)
    {
        EquipItem equip = item.ConvertToEquip();
        if (equip == null)
            return false;

        Item before = equipList[equip.Type];                                // 장비 아이템을 꺼낸다.
        equipList[equip.Type] = null;                                       // 기존의 장비 배열에서 제거한다.

        Vector3 itemPos = transform.position + (transform.forward * 2f);    // 나의 전방 2미터 앞.
        ItemManager.Instance.ConvertToObject(before, itemPos);              // 땅바닥에 버린다.
        
        return true;
    }
    public void OnDamaged(float damage)
    {
        // 맞는 모션 취하기.
        StateInfoUI.Instance.SetHp(stat.hp, stat.MaxHp);
    }
    public void OnDead()
    {
        Debug.Log("플레이어 사망..");
        isAlive = false;
    }

    private void OnDrawGizmosSelected()
    {
        if(searchItemPivot != null)
            Gizmos.DrawWireSphere(searchItemPivot.position, searchRadius);
    }

    public Vector3 RemoveItemPosition()
    {
        return transform.position + (transform.forward * 2f);
    }
}
