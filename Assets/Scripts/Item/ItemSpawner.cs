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
        // �� �ڽ�(= ��ġ ����)�� ����.
        itemPivots = new Transform[transform.childCount];
        for (int i = 0; i < itemPivots.Length; i++)
            itemPivots[i] = transform.GetChild(i);


        // �� itemPivots��ŭ �������� �����Ѵ�.
        foreach(Transform pivot in itemPivots)
        {
            // 50% Ȯ���� �ش� pivot���� �������� �ʴ´�.
            if (Random.value < 0.5f)
                continue;

            // � �������� ������ΰ�?
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
