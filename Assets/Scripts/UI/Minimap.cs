using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    [SerializeField] Camera minimapCam;
    [SerializeField] Slider slider;
    [SerializeField] Text sizeText;
    [SerializeField] float minSize;
    [SerializeField] float maxSize;

    float offset;
    private void Start()
    {
        RandomBox<string> randomBox = new RandomBox<string>();
        randomBox.Push("5.56mm", 50);
        randomBox.Push("7.62mm", 50);
        randomBox.Push("1·¹º§ Çï¸ä", 30);
        randomBox.Push("2·¹º§ Çï¸ä", 20);
        randomBox.Push("3·¹º§ Çï¸ä", 10);

        for (int i = 0; i < 10000; i++)
            Debug.Log("¾ÆÀÌÅÛ »Ì±â : " + randomBox.Pick());


        offset = (maxSize - minSize) / 10.0f;
    }

    public delegate void OnValueChanged(float value);
    OnValueChanged OnValueChangedEvent;

    // Mathf.Round : ¹Ý¿Ã¸². (float·Î ¹ÝÈ¯)
    // Mathf.Ceil  : ¿Ã¸².
    // Mathf.Floor : ³»¸².

    // Mathf.RoundToInt : ¹Ý¿Ã¸² ÈÄ¿¡ int·Î ¹ÝÈ¯.
    bool isButton;      // ¹öÆ°À» ´­·¶´Â°¡?

    public void OnChangedSlider(float value)
    {
        if (isButton == false)
        {
            // value : 0 ~ 10.
            float camSize = minSize + (offset * value);
            OnUpdateMinimap(camSize);
        }

        isButton = false;
    }

    public void OnSizeUp()
    {
        float size = Mathf.Clamp(minimapCam.orthographicSize - offset, minSize, maxSize);
        OnUpdateMinimap(size);
    }
    public void OnSizeDown()
    {
        float size = Mathf.Clamp(minimapCam.orthographicSize + offset, minSize, maxSize);
        OnUpdateMinimap(size);
    }

    private void OnUpdateMinimap(float size)
    {
        isButton = true;
        minimapCam.orthographicSize = size;

        float persent = (size - minSize) / (maxSize - minSize) * 100.0f;
        sizeText.text = string.Format("{0}%", Mathf.Round(persent));

        slider.value = (int)((size - minSize) / offset);
    }
}
