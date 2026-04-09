using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Respawn Settings")]
    [SerializeField] private Transform spawnPoint;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void RespawnPlayer(GameObject player)
    {
        if (player == null)
        {
            return;
        }

        Vector3 respawnPosition = spawnPoint != null ? spawnPoint.position : transform.position;
        player.transform.position = respawnPosition;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.HandleRespawn();
        }
    }
}
