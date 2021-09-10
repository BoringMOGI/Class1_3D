using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponGun : MonoBehaviour
{
    [Header("Bulelt")]
    [SerializeField] AmmoItem.AMMOTYPE ammoType;    // 탄약 종류.
    [SerializeField] Bullet bulletPrefab;           // 탄약 프리팹.
    [SerializeField] Transform muzzle;              // 총구 위치.
    [SerializeField] float bulletSpeed;             // 탄약 속도.

    [Header("Gun")]
    [SerializeField] Animator anim;                 // 애니메이션.
    [SerializeField] AudioSource fireAudio;         // 발사 효과음.
    [SerializeField] float fireRate;                // 공격 속도.
    [SerializeField] int maxAmmoCount;              // 총 장전가능한 총알.
    [SerializeField] Vector2 gunRecoil;             // 총기 반동.
    [SerializeField] float gunPower;                // 총기 데미지.

    public UnityEvent<float, float> OnAddRecoil;    // 총기 반동 이벤트.
    public UnityEvent<int, int> OnUpdateAmmoUI;     // 탄약의 개수를 그리는 UI 갱신.

    int ammoCount;                                  // 남은 총알의 개수.
    float nextFireTime;                             // 발사 가능 시간.

    bool isReloading;                               // 장전 중인가?
    Inventory inven;                                // 인벤토리.

    private int AmmoCount
    {
        get
        {
            return ammoCount;
        }
        set
        {
            // 탄약의 값이 변경 되었다.
            ammoCount = Mathf.Clamp(value, 0, maxAmmoCount);
            OnUpdateAmmoUI?.Invoke(ammoCount, inven.ItemCount(ammoType));
        }
    }

    private void Start()
    {
        inven = Inventory.Instance;
        inven.OnUpdateInventory += OnUpdatedInven;

        AmmoCount = 0;

        OnInit();
    }
    private void OnInit()
    {

    }
    private void OnUpdatedInven()
    {
        // 인벤토리의 아이템들이 변화하면 UI를 갱신한다.
        OnUpdateAmmoUI?.Invoke(ammoCount, inven.ItemCount(ammoType));
    }

    public void Fire(Vector3 hitPoint)
    {
        if(nextFireTime <= Time.time && ammoCount > 0 && isReloading == false)
        {
            AmmoCount -= 1;

            anim.SetTrigger("onFire");
            fireAudio.Play();
            nextFireTime = Time.time + fireRate;
            Bullet newBullet = Instantiate(bulletPrefab, muzzle.position, Quaternion.LookRotation(hitPoint - muzzle.position));
            newBullet.Shoot(bulletSpeed, gunPower);

            // 반동.
            OnAddRecoil?.Invoke(Random.Range(-gunRecoil.x, gunRecoil.x), gunRecoil.y);
            //MouseLook.Instance.AddRecoil(Random.Range(-gunRecoil.x, gunRecoil.x), gunRecoil.y);
        }
    }

    public void Reload()
    {
        // 리로딩 중이 아닐 경우.
        // 현재 탄약이 최대 탄약보다 작을 경우.
        // 인벤토리에 탄약이 1발이라도 있을 경우.
        if (isReloading == false && ammoCount < maxAmmoCount && inven.IsEnougthItem(ammoType, 1))
        {
            isReloading = true;
            anim.SetTrigger("onReload");
        }
    }
    public void OnEndReload()
    {
        isReloading = false;

        int needCount = maxAmmoCount - ammoCount;           // 필요 탄약.
        int getItem = inven.GetItem(ammoType, needCount);   // 수령 탄약.

        AmmoCount += getItem;
    }

}
