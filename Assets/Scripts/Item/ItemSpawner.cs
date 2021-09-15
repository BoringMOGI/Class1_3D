using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{    
    Transform[] itemPivots;

    // Start is called before the first frame update
    void Start()
    {
        // 내 자식(= 위치 지점)을 대입.
        itemPivots = new Transform[transform.childCount];
        for (int i = 0; i < itemPivots.Length; i++)
            itemPivots[i] = transform.GetChild(i);


        // 총 itemPivots만큼 아이템을 생성한다.
        foreach(Transform pivot in itemPivots)
        {
            // 50% 확률로 해당 pivot에는 등장하지 않는다.
            if (Random.value < 0.5f)
                continue;

            Spawn(pivot);
        }
    }

    void Spawn(Transform pivot)
    {
        ItemObject newItem = ItemManager.Instance.GetRandomItemObject();
        newItem.transform.position = pivot.transform.position;
    }
}
