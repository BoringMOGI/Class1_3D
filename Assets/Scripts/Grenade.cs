using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] float delay;               // 몇 초 뒤에 떠지는가?
    [SerializeField] float explodeRadius;       // 폭발 범위.
    [SerializeField] ParticleSystem effect;     // 폭발 이펙트.

    float countDown = 0.0f;
    bool isLock = true;                         // 최초에는 잠겨있다.

    void Start()
    {
        isLock = true;
    }


    public void Unlock()
    {
        if (isLock == false)
            return;

        Debug.Log("Grenade Unlock!");
        isLock = false;
        countDown = Time.time + delay;          // 현재 게임의 시간 + dealy.
    }


    private void Update()
    {
        if (isLock)                             // lock상태에서는 Update를 실행하지 않는다.
            return;

        if(countDown <= Time.time)              // 게임의 시간이 countDown을 넘었을 경우.
        {
            Debug.Log("Explode!!");
            Instantiate(effect, transform.position, transform.rotation);        // 폭발 이펙트 생성.
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explodeRadius);
    }

}
