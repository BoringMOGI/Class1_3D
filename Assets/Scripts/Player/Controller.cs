using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] protected WeaponGun mainGun;
    [SerializeField] protected Transform eyePivot;

    protected void Fire()
    {
        Vector3 hitPoint = Vector3.zero;    // 시야 기준 조준점 직선 방향에 맞은 곳.

        Ray ray = new Ray(eyePivot.position, eyePivot.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            hitPoint = hit.point;
        }
        else
        {
            hitPoint = eyePivot.forward * 1000f;
        }

        mainGun.Fire(hitPoint);
    }

}
