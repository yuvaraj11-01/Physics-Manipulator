using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceDirectionValue : PropertyValue
{
    int angle;
    [SerializeField] RectTransform arrow;

    private void Awake()
    {
        initVisual();
    }

    public void OnChange()
    {
        angle += 45;
        initVisual();
    }

    void initVisual()
    {
        //Quaternion newRo = 
        arrow.rotation = Quaternion.Euler(0, 0, angle); //.SetEulerAngles( new Vector3(0, 0, angle));
    }
}
