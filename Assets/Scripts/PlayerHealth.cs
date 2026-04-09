using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private LiveBar liveBar;
    [SerializeField] private float maxHearts= 3f;
    private float currentHearts;

    private void Start()
    {
        currentHearts = maxHearts;
    }

    public void TakeDamage(float damage)
    {
        currentHearts -= damage;
        liveBar.MinusHeart();
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RespawnPlayer(gameObject);
        }

        if (currentHearts <= 0)
        {
            // Handle player death
        }
    }
    public float GetCurrentHearts()
    {
        return currentHearts;
    }
}
