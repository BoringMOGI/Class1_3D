using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] Transform target;      // 따라다닐 대상.
    [SerializeField] Vector3 offset;        // 대상으로부터의 위치.
    [SerializeField] bool fixX;             // x축을 맞추겠다.
    [SerializeField] bool fixY;             // y축을 맞추겠다.
    [SerializeField] bool fixZ;             // z축을 맞추겠다.


    // Update() 이후에 호출.
    private void LateUpdate()
    {
        Vector3 pos = transform.position;

        if (fixX)
            pos.x = target.position.x + offset.x;
        if (fixY)
            pos.y = target.position.y + offset.y;
        if (fixZ)
            pos.z = target.position.z + offset.z;

        transform.position = pos;
    }
}
