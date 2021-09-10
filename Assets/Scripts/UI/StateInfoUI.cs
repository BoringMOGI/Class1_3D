using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateInfoUI : Singletone<StateInfoUI>
{
    [SerializeField] Image hpImage;
    [SerializeField] Image staminaImage;
    [SerializeField] Text currentBulletText;
    [SerializeField] Text maxBulletText;

    protected new void Awake()
    {
        base.Awake();

        hpImage.fillAmount = 1f;
        staminaImage.fillAmount = 0f;
        currentBulletText.text = "0";
        maxBulletText.text = "0";
    }

    public void SetHp(float current, float max)
    {
        hpImage.fillAmount = current / max;         // 0.0 ~ 1.0¿« ∫Ò¿≤.
    }
    public void SetStamina(float current, float max)
    {
        staminaImage.fillAmount = current / max;
    }
    public void SetBullet(int current, int max)
    {
        currentBulletText.text = current.ToString();
        maxBulletText.text = max.ToString();
    }
}
