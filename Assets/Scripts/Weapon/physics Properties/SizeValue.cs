using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeValue : PropertyValue
{
    bool _value;
    [SerializeField] TMPro.TMP_Text text;

    private void Awake()
    {
        initVisual();

    }

    public override void init()
    {
        base.init();
        if(Weapon.propertyQueue == null) { Weapon.propertyQueue = new List<IProperty>() { null, null, null }; }
        Weapon.propertyQueue[slot.slotIndex] = new SetSize(this);

    }

    public void OnChange()
    {
        _value = !_value;
        initVisual();
    }

    void initVisual()
    {
        text.text = _value ? "Inc" : "Dec";
    }

    public bool GetValue()
    {
        return _value;
    }

    private void OnDestroy()
    {
        Weapon.propertyQueue[slot.slotIndex] = null;
    }

}
