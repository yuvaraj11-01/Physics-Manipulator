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
    int maxReflections = 10;
    int currentReflections = 0;
    int defaultRayDistance = 100;

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
        var hitpos = new List<Vector2>();
        hitpos = Shoot(shootPos.position, mousePos - shootPos.position, out Rigidbody2D rb);
        // Draw line renderer to the target pos
        var visual = Instantiate(laserVisual).GetComponent<LineRenderer>();
        visual.useWorldSpace = true;

        visual.positionCount = hitpos.Count;
        for (int i = 0; i < hitpos.Count; i++)
        {
            visual.SetPosition(i, hitpos[i]);
            Debug.Log(hitpos[i]);
        }
        Debug.Log(hitpos.Count +" : "+rb.name);
        

        Destroy(visual.gameObject, .2f);

        // apply properties
        if (rb)
        {
            StartCoroutine( ApplyQueueProperties(rb));
        }
    }

    List<Vector2> Shoot(Vector3 Pos,Vector3 dir, out Rigidbody2D rb)
    {
        List<Vector2> Points = new List<Vector2>();
        var hit = Physics2D.Raycast(Pos, dir, float.MaxValue, detectLayer);

        currentReflections = 0;
        Points.Clear();
        Points.Add(Pos);

        if (hit.collider != null)
        {
            if(hit.collider.tag == "Reflector")
            {
                var reflectedDir =  Vector2.Reflect(dir, hit.normal);
                ReflectFurther(Points, Pos, hit, out rb);
            }
            else
            {
                Points.Add(hit.point);
                rb = hit.rigidbody;
            }
            Debug.Log($"{hit.collider.name} - {hit.point}");
            Debug.DrawLine(shootPos.position, hit.point, Color.green, .25f);
            
        }
        else
        {

            //Debug.DrawLine(shootPos.position, mousePos, Color.red, .25f);
            Points.Add(Pos + dir * 999);
            rb = null;
        }

        return Points;
    }

    private void ReflectFurther(List<Vector2> Points,Vector2 origin, RaycastHit2D hitData, out Rigidbody2D rb)
    {
        rb = null;
        if (currentReflections > maxReflections)                                                                                                                                                  
        {
            
            return;
        }
        Points.Add(hitData.point);
        currentReflections++;

        Vector2 inDirection = (hitData.point - origin).normalized;
        Vector2 newDirection = Vector2.Reflect(inDirection, hitData.normal);

        var newHitData = Physics2D.Raycast(hitData.point + (newDirection * 0.0001f), newDirection * 100, 900, detectLayer);
        if (newHitData)
        {
            if (newHitData.collider.tag == "Reflector")
                ReflectFurther(Points, hitData.point, newHitData, out rb);
            else
            {
                Points.Add(newHitData.point);// + newDirection * defaultRayDistance);
                rb = newHitData.rigidbody;
            }
        }
        else
        {
            Points.Add(hitData.point + newDirection * defaultRayDistance);
        }
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
