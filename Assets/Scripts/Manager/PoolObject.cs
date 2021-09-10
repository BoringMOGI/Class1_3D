using System;
using UnityEngine;

public class PoolObject<T> : MonoBehaviour, IPool<T>
{
    public Action<T> OnReturnPool;
    private T component;

    public void OnReturnForce()
    {
        OnReturnPool.Invoke(component);
    }

    public void Setup(Action<T> OnReturnPool)
    {
        this.OnReturnPool = OnReturnPool;
        component = GetComponent<T>();
    }
}
