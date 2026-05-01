using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Respawn Settings")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float cannonRefireDelay = 1f;

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

        RespawnLevelObjects();

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

    private void RespawnLevelObjects()
    {
        LevelRespawnable[] respawnables = FindObjectsOfType<LevelRespawnable>(true);
        for (int i = 0; i < respawnables.Length; i++)
        {
            respawnables[i].Respawn();
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>(true);
        for (int i = 0; i < bombs.Length; i++)
        {
            if (!bombs[i].gameObject.activeSelf)
            {
                bombs[i].gameObject.SetActive(true);
            }

            bombs[i].ResetState();
        }

        CannonBall[] cannonBalls = FindObjectsOfType<CannonBall>(true);
        for (int i = 0; i < cannonBalls.Length; i++)
        {
            cannonBalls[i].Despawn();
        }

        Cannon[] cannons = FindObjectsOfType<Cannon>(true);
        for (int i = 0; i < cannons.Length; i++)
        {
            cannons[i].ResetFireTimer(cannonRefireDelay);
        }
    }

}
