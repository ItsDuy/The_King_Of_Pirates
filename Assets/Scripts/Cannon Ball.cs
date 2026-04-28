using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    // [SerializeField] private float dir = -1f; // Direction of the cannonball, -1 for left, 1 for right
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float damage =1f;
    
    private Rigidbody2D rb;
    private float direction;
    public void SetDirection(float dir)
    {
        direction = dir;
    }
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer.value) != 0)
        {
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        
        else if (((1 << collision.gameObject.layer) & wallLayer.value) != 0)
        {
            // Optionally, you can add effects or sounds here when the cannonball hits a wall
            Destroy(gameObject);
        }
    }
    

    private void Update()
    {
        rb.velocity = new Vector2(transform.right.x * speed * direction, transform.right.y);
    }
}
