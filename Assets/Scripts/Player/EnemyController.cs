using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Controller
{
    private void Start()
    {
        Invoke("OnFire", 1.0f);
    }

    void OnFire()
    {
        Fire();
        Invoke("OnFire", 1.0f);
    }
}
