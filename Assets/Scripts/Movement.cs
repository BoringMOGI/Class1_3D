using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Rigidbody rigid;
    [SerializeField] float moveSpeed;

    public void OnMovement(Vector2 movement)
    {
        transform.Translate(new Vector3(movement.x, 0f, movement.y) * moveSpeed * Time.deltaTime);
    }
    public void OnRatate(Vector2 movement)
    {

    }
}
