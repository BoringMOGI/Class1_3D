using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisable : MonoBehaviour
{
    [SerializeField] Behaviour[] components;

    void Start()
    {
        foreach(Behaviour component in components)
            component.enabled = false;
    }
}
