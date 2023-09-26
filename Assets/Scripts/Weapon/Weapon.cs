using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Weapon : MonoBehaviour
{
    public static  List<IProperty> propertyQueue = new List<IProperty>() { null, null, null };

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
        Shoot(out Vector3 hitpos, out Rigidbody2D rb);
        // Draw line renderer to the target pos
        // apply properties
        if (rb)
        {
            StartCoroutine( ApplyQueueProperties(rb, hitpos));
        }
    }

    void Shoot(out Vector3 res, out Rigidbody2D rb)
    {
        var mousePos = MouseInput.GetPlayerMousePos(out bool inBounds);

        if (!inBounds)
        {
            res = Vector3.zero;
            rb = null;
            return;
        }

        var dir = mousePos - shootPos.position;

        var hit = Physics2D.Raycast(shootPos.position, dir, float.MaxValue, detectLayer);

        if(hit.collider != null)
        {
            Debug.Log($"{hit.collider.name} - {hit.point}");
            Debug.DrawLine(shootPos.position, hit.point, Color.green, .25f);
            res = hit.point;
            rb = hit.rigidbody;
        }
        else
        {
            Debug.DrawLine(shootPos.position, mousePos, Color.red, .25f);
            res = mousePos;
            rb = null;
        }
    }

    IEnumerator ApplyQueueProperties(Rigidbody2D rb, Vector2 hitPos)
    {
        foreach (var property in propertyQueue)
        {
            Debug.Log("exe");
            if (property == null) continue;

            property.Execute(rb, hitPos);
            Debug.Log("exe Done");
            yield return null;
        }
    }


}
