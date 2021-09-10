using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(OnStart());
    }

    IEnumerator OnStart()
    {
        bool[] beforeActive = new bool[transform.childCount];       // 자식들의 이전 상태 저장.
        for(int i = 0; i<transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;    // i번째 자식 오브젝트.
            beforeActive[i] = child.activeSelf;                     // 이전 상태 저장.
            child.SetActive(true);                                  // 자식 오브젝트 활성화.
        }
        
        yield return new WaitForEndOfFrame();                       // 1프레임 끝날때까지 기다림.

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.SetActive(beforeActive[i]);                       // 이전 상태로 돌리기.
        }
    }

}
