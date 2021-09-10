using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] Item hasItem;

    public Item HasItem
    {
        get
        {
            return hasItem.GetCopy();
        }
        set
        {
            hasItem = value;
        }
    }

}
