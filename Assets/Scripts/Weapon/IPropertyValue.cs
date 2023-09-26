using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class PropertyValue : MonoBehaviour
{
    [HideInInspector] public PropertySlot slot;
    public PhysicsProperty propertyType;
    public virtual void init()
    {
        
    }
}