using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GravityValue : PropertyValue
{
    bool _value;
    [SerializeField] TMPro.TMP_Text text;

    private void Awake()
    {
        initVisual();
    }

    public void OnChange()
    {
        _value = !_value;
        initVisual();
    }

    void initVisual()
    {
        text.text = _value ? "ON" : "OFF";
    }

}
