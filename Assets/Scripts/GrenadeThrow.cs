using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{
    [SerializeField] Grenade grenadePrefab;
    [SerializeField] Transform throwPivot;
    [SerializeField] float throwPower;

    Grenade grenade = null;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("폭탄 생성 및 준비 모션 재생");
            Debug.Log("무언가 효과음");
            CreateGrenade();
        }
        if(Input.GetMouseButtonUp(0))     // 마우스 왼쪽 클릭.
        {
            Throw();                      // 폭탄 생성 후 던지기.
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(grenade != null)
                grenade.Unlock();
        }

        // 수류탄은 pivot의 위치를 계속 따라다녀야 한다.
        if (grenade != null)
        {
            grenade.transform.position = throwPivot.position;
            grenade.transform.rotation = throwPivot.rotation;
        }
    }

    void CreateGrenade()
    {
        // 수량 체크.
        // 내 남은 수류탄이 없다면...

        grenade = Instantiate(grenadePrefab, throwPivot.position, throwPivot.rotation);
        grenade.GetComponent<Rigidbody>().useGravity = false;
    }
    void Throw()
    {
        if (grenade == null)
            return;

        // 프리팹 클론 생성 후 전방으로 throwPower의 힘 만큼 던진다.
        grenade.Unlock();
        grenade.GetComponent<Rigidbody>().AddForce(transform.forward * throwPower, ForceMode.Impulse);
        grenade.GetComponent<Rigidbody>().useGravity = true;
        grenade = null;
    }
}
