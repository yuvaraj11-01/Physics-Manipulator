using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGravityProperty : MonoBehaviour, IPhysicsProperty
{
    public PhysicsProperty physicsProperty { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void Execute(Rigidbody2D rb)
    {
        rb.gravityScale = 1;
    }
}
