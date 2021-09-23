using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Joystick : MonoBehaviour
{
    [SerializeField] RectTransform joystick;
    [SerializeField] protected UnityEvent<Vector2> OnMoveStick;
    

    Vector2 originPosition;         // 스틱의 초기 위치.
    float stickRadius;              // 스틱의 반지름.
    bool isModify;                  // 조작 여부.

    private void OnEnable()
    {
#if UNITY_STANDALONE
    gameObject.SetActive(false);
#else
        originPosition = joystick.position;
        stickRadius = GetComponent<RectTransform>().sizeDelta.x / 2f;   // 스틱의 경계 너비 / 2f.
#endif
    }

    public void OnBeginModify()
    {
        isModify = true;
    }
    public void OnEndModify()
    {
        isModify = false;
        joystick.position = originPosition;     // 스틱을 원래 위치로 되돌림.
    }

    void Update()
    {
        if (!isModify)
            return;

        // Vector2.normalized : (0.0~1.0)사이의 정규화 된 값을 리턴하는 프로퍼티.
        // Vector.Distance(V, V) : 두 벡터 사이의 거리 값을 flaot로 리턴.
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 direction = (mousePosition - originPosition).normalized;
        float distance = Vector2.Distance(originPosition, mousePosition);
        distance = Mathf.Clamp(distance, 0f, stickRadius);      // 거리의 값은 0~반지름.

        // 방향 * 거리 = 이동량.
        joystick.position = originPosition + (direction * distance);

        float stickPower = distance / stickRadius;              // 스틱을 얼마나 움직였는가?(0.0~1.0);
        OnMoveStick?.Invoke(direction * stickPower);            // 움직임을 비율대로 전달.
        //Debug.Log($"distance:{distance}, vector:{direction}");
    }
}
