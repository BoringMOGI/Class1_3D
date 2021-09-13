using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{    
    [SerializeField] ItemObject[] spawnItems;
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

            // 어떤 아이템을 만들것인가?
            ItemObject item = spawnItems[Random.Range(0, spawnItems.Length - 1)];
            Spawn(item, pivot);
        }
    }

    void Spawn(ItemObject item, Transform pivot)
    {
        ItemObject newItem = Instantiate(item, transform);
        newItem.transform.position = pivot.transform.position;
    }
}
