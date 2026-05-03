using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LevelRespawnable))]
public class Door : MonoBehaviour, IRespawnResettable
{
    private Animator anim;
    [SerializeField] private Chest chest; // Reference to the chest that controls the door
    [SerializeField] private BoxCollider2D doorCollider; // Collider to block the player when the door is closed
    [SerializeField] private LayerMask playerLayer; // Layer mask to identify the player
    private void Start()
    {
        anim = GetComponent<Animator>();
        doorCollider.enabled = false;
    }

    private void Update()
    {
        if (chest != null && chest.IsOpen())
        {
            anim.SetTrigger("isOpened");
            doorCollider.enabled = true; // Enable the collider to allow the player to pass through
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer.value) != 0)
        {
            if (chest != null && chest.IsOpen())
            { 
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
            }
        }
    }

    public void ResetState()
    {
        if (doorCollider != null)
        {
            doorCollider.enabled = false;
        }
    }
}
