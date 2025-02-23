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

    public override void init()
    {
        base.init();
        if (Weapon.propertyQueue == null) { Weapon.propertyQueue = new List<IProperty>() { null, null, null }; }
        Weapon.propertyQueue[slot.slotIndex] = new AddFroce(this);

    }

    public void OnChange()
    {
        angle += 45;
        initVisual();
    }

    void initVisual()
    {
        arrow.rotation = Quaternion.Euler(0, 0, angle);
    }


    public int GetValue()
    {
        return angle;
    }

    private void OnDestroy()
    {
        Weapon.propertyQueue[slot.slotIndex] = null;
    }

}
