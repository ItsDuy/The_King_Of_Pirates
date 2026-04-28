using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject cannonBallPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 10f; // Time in seconds between shots
    [SerializeField] private float direction = -1f; // Direction of the cannonball, -1 for left, 1 for right
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private float nextFireTime = 0.5f;

   

    private void Update()
    {
        if (Time.time >= nextFireTime)
        {
            FireCannonBall();
            nextFireTime = Time.time + fireRate;
        }
    }
    private void FireCannonBall()
    {
    anim.SetTrigger("Fire");
    var cannonBall = Instantiate(cannonBallPrefab, firePoint.position, firePoint.rotation);
    cannonBall.GetComponent<CannonBall>().SetDirection(direction);
    
    }
}
