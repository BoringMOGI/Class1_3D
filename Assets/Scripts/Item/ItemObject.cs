using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] Transform spriteObject;

    public Item HasItem;
    Camera eyeCam;

    public void Setup(Item hasItem)
    {
        HasItem = hasItem;

        spriteObject.GetComponent<SpriteRenderer>().sprite = HasItem.itemSprite;

        eyeCam = Camera.main;

        StartCoroutine(Fly());
    }
    IEnumerator Fly()
    {
        Vector3 downPosition = spriteObject.transform.localPosition;        // 최초 위치.
        Vector3 upPosition = downPosition + (Vector3.up * 0.2f);            // 최초 위치에서 위로 0.5.

        bool isDown = true;
        while(true)
        {
            isDown = !isDown;
            Vector3 destination = isDown ? downPosition : upPosition;

            // 목적지로 이동.
            while (spriteObject.localPosition != destination)
            {
                Vector3 pos = Vector3.MoveTowards(spriteObject.localPosition, destination, 0.5f * Time.deltaTime);
                spriteObject.localPosition = pos;

                yield return null;
            }

            // 도착하면 0.1초 대기.
            // yield return new WaitForSeconds(0.1f);
        }
    }

    void Update()
    {
        transform.LookAt(eyeCam.transform.position);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
