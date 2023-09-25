using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPhysicsProperty
{
    PhysicsProperty physicsProperty { get; set; }
    void Execute(Rigidbody2D rb);
}


public enum PhysicsProperty
{
    None,
    Gravity,
    Force,
    Size
}