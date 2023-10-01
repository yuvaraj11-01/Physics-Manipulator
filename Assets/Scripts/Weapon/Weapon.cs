using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Weapon : MonoBehaviour
{
    public static  List<IProperty> propertyQueue = new List<IProperty>() { null, null, null };

    [SerializeField] Transform shootPos;
    [SerializeField] LayerMask detectLayer;
    [SerializeField] Transform laserVisual;

    InputController inputs;

    private void Awake()
    {
        inputs = new InputController();
        inputs.Player.Enable();
    }

    private void OnEnable()
    {
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
        var mousePos = MouseInput.GetPlayerMousePos(out bool inBounds);

        if (!inBounds) return;

        //Debug.Log("Firing");
        // shoot ray and get target pos and target collider
        var hitpos = Shoot(shootPos.position, mousePos - shootPos.position, out Rigidbody2D rb);
        // Draw line renderer to the target pos
        var visual = Instantiate(laserVisual).GetComponent<LineRenderer>();
        visual.useWorldSpace = true;

        visual.positionCount = hitpos.Count+1;
        visual.SetPosition(0, shootPos.position);
        for (int i = 0; i < hitpos.Count; i++)
        {
            visual.SetPosition(i+1, hitpos[i]);
        }
        Debug.Log(hitpos.Count);
        

        Destroy(visual.gameObject, .2f);

        // apply properties
        if (rb)
        {
            StartCoroutine( ApplyQueueProperties(rb));
        }
    }

    List<Vector2> Shoot(Vector3 Pos,Vector3 dir, out Rigidbody2D rb)
    {
        List<Vector2> res = new List<Vector2>();
        var hit = Physics2D.Raycast(Pos, dir, float.MaxValue, detectLayer);

        if(hit.collider != null)
        {
            if(hit.collider.tag == "Reflector")
            {
                var reflectedDir =  Vector2.Reflect(dir, hit.normal);
                //res.Add(hit.point);
                res.Concat(Shoot(hit.point ,reflectedDir, out rb));
                
            }
            Debug.Log($"{hit.collider.name} - {hit.point}");
            Debug.DrawLine(shootPos.position, hit.point, Color.green, .25f);
            res.Add(hit.point);
            rb = hit.rigidbody;
        }
        else
        {

            //Debug.DrawLine(shootPos.position, mousePos, Color.red, .25f);
            res.Add(dir);
            rb = null;
        }
        return res;
    }

    IEnumerator ApplyQueueProperties(Rigidbody2D rb)
    {
        foreach (var property in propertyQueue)
        {
            Debug.Log("exe");
            if (property == null) continue;

            property.Execute(rb);
            Debug.Log("exe Done");
            yield return null;
        }
    }


}
