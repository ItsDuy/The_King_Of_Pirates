using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject cannonBallPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 10f; // Time in seconds between shots
    [SerializeField] private float direction = -1f; // Direction of the cannonball, -1 for left, 1 for right
    [SerializeField] private SfxManager sfxManager;
    [SerializeField] private AudioClip fireClip;
    private Animator anim;
    private float FireAnimation = 1f;
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
        StartCoroutine(FireAnimationCoroutine());

        GameObject cannonBallObject = Instantiate(cannonBallPrefab, firePoint.position, firePoint.rotation);
        CannonBall cannonBall = cannonBallObject.GetComponent<CannonBall>();
        cannonBall.SetDirection(direction);
    }

    private IEnumerator FireAnimationCoroutine()
    {   
        anim.SetTrigger("Fire");
        sfxManager.Play(fireClip);
        yield return new WaitForSeconds(FireAnimation);
    }
    public void ResetFireTimer(float delay)
    {
        nextFireTime = Time.time + delay;
    }
}
