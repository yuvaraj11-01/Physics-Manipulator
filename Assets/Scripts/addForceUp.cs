using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addForceUp : MonoBehaviour
{
    [SerializeField] float Force = 1600;
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
        if(collision.tag == "Player" || collision.tag == "PhysicsObj") 
        {
            var rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.AddForce(transform.up * Force);
        }
    }
}
