using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IRespawnResettable
{
    [SerializeField] private LayerMask playerLayer; // Layer mask to identify the player
    [SerializeField] private LayerMask destructibleLayer; // Layer mask to identify destructible objects
    [SerializeField] private float explosionRadius = 5f; // Radius of the explosion
    [SerializeField] private float timeExplosion = 1f; // Time before the bomb explodes
    [SerializeField] private float explosionDamage = 1f; // Damage dealt by the explosion
    // Start is called before the first frame update
    private Animator anim;
    private bool isExploding;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isExploding)
        {
            return;
        }

        if (((1 << collision.gameObject.layer) & playerLayer.value) != 0)
        {
            StartCoroutine(Explosion());
        }
    }
    private IEnumerator Explosion()
    {
        isExploding = true;
        if (anim != null)
        {
            anim.SetTrigger("On");
        }
        yield return new WaitForSeconds(timeExplosion); // Wait for the specified time before dealing damage
        ExplosionDamage(); // Deal damage to nearby objects
        if (!isExploding)
        {
            yield break;
        }
        gameObject.SetActive(false);
    }

    public void ResetState()
    {
        StopAllCoroutines();
        isExploding = false;
        if (anim != null)
        {
            anim.Rebind();
            if (gameObject.activeInHierarchy)
            {
                anim.Update(0f);
            }
        }
    }
    void ExplosionDamage()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, playerLayer | destructibleLayer);
        foreach (Collider2D hitCollider in hitColliders)
        {
            IDamageable damageable = hitCollider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(explosionDamage);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
