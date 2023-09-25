using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunHolder : MonoBehaviour
{
    [SerializeField] Transform playerVisual;
    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        RotateHolder();
    }

    public void RotateHolder()
    {
        Vector3 mousePosition = MouseInput.GetPlayerMousePos(out bool inBound);
        mousePosition.z = 0;

        if (!inBound) return;


        Vector3 direction = mousePosition - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (transform.position.x > mousePosition.x)
        {
            transform.localScale = new Vector3(1, -1, 1);
            playerVisual.localScale = new Vector3(-1, 1, 1);
        }
        else if (transform.position.x < mousePosition.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            playerVisual.localScale = new Vector3(1, 1, 1);
        }
    }

}
