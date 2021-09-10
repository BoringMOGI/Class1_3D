using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassUI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] RectTransform compassImage;
    [SerializeField] RectTransform leftImage;
    [SerializeField] RectTransform rightImage;

    float targetRotation => target.eulerAngles.y;         // 내가 기준으로 삼은 타겟의 회전 값.
    float compassLength => compassImage.sizeDelta.x;      // 나침반 이미지의 길이.
    
    private void Start()
    {
        gameObject.SetActive(true);
        ShortcutManager.Instance.RegestedShutcut(OnSwitchCompass);
    }
    private void OnDestroy()
    {
        ShortcutManager.Instance.RemoveShortcut(OnSwitchCompass);
    }

    void Update()
    {
        RotateCompass();
    }

    private void RotateCompass()
    {
        float positionX = (targetRotation / 360.0f) * compassLength * -1f;

        compassImage.localPosition = new Vector3(positionX, 0f, 0f);                 // 나침반의 X축 위치.
        leftImage.localPosition = compassImage.localPosition;                        // 왼쪽 보조 이미지의 X축 위치.
        rightImage.localPosition = new Vector3(positionX + compassLength, 0f, 0f);   // 오른쪽 보조 이미지의 X축 위치.
    }

    public void OnSwitchCompass(KeyCode key)
    {
        if (key != KeyCode.Tab)
            return;

        gameObject.SetActive(!gameObject.activeSelf);
    }
}
