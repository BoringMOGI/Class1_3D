using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField] Camera minimapCam;
    [SerializeField] float offset;
    [SerializeField] float minSize;
    [SerializeField] float maxSize;


    public void OnSizeUp()
    {
        minimapCam.orthographicSize = Mathf.Clamp(minimapCam.orthographicSize - offset, minSize, maxSize); 
    }
    public void OnSizeDown()
    {
        minimapCam.orthographicSize = Mathf.Clamp(minimapCam.orthographicSize + offset, minSize, maxSize);
    }
}
