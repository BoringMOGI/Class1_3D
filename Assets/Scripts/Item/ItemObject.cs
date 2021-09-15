using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public Item HasItem;

    Camera eyeCam;

    public void Setup(Item hasItem)
    {
        HasItem = hasItem;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = HasItem.itemSprite;

        eyeCam = Camera.main;
    }

    void Update()
    {
        transform.LookAt(eyeCam.transform.position);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
