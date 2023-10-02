using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGravity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "PhysicsObj") 
        {
            var rb = collision.GetComponent<Rigidbody2D>();
            if (rb.gravityScale == 0)
            {
                rb.gravityScale = 5;
            }
            else
                rb.gravityScale = 0;
        }
    }
}
