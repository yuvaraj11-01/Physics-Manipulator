using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Weapon : MonoBehaviour
{
    [SerializeField] Transform shootPos;
    [SerializeField] LayerMask detectLayer;

    InputController inputs;

    private void Awake()
    {
        inputs = new InputController();
        inputs.Player.Enable();
        inputs.Player.Fire.performed += Fire_performed;
    }

    private void Fire_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        PerformFire();
    }

    private void OnDisable()
    {
        inputs.Player.Fire.performed -= Fire_performed;
    }

    void PerformFire()
    {
        //Debug.Log("Firing");
        // shoot ray and get target pos and target collider
        Shoot(out Vector3 hitpos, out Collider2D hitCollider);
        // Draw line renderer to the target pos

    }

    void Shoot(out Vector3 res, out Collider2D resCollider)
    {
        var mousePos = MouseInput.GetPlayerMousePos(out bool inBounds);

        if (!inBounds)
        {
            res = Vector3.zero;
            resCollider = null;
            return;
        }

        var dir = mousePos - shootPos.position;

        var hit = Physics2D.Raycast(shootPos.position, dir, float.MaxValue, detectLayer);

        if(hit.collider != null)
        {
            Debug.Log($"{hit.collider.name} - {hit.point}");
            Debug.DrawLine(shootPos.position, hit.point, Color.green, .25f);
            res = hit.point;
            resCollider = hit.collider;
        }
        else
        {
            Debug.DrawLine(shootPos.position, mousePos, Color.red, .25f);
            res = mousePos;
            resCollider = null;
        }
    }


}
