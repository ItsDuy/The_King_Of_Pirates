using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Inventory playerInventory; // Reference to the player's inventory
    [SerializeField] private int keyNeeded = 1; // Number of keys needed to open the chest
    private Animator anim;
    private bool isOpen = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer.value) != 0)
        {
            if (playerInventory != null)
            {
                if (playerInventory.GetKeys() >= keyNeeded)
                {
                    playerInventory.AddKeys(-keyNeeded); // Remove the required keys from the inventory
                    anim.SetTrigger("Open"); // Play the opening animation
                    isOpen = true;
                    // Optionally, you can add rewards or effects here when the chest is opened
                }
                else
                {
                    Debug.Log("Not enough keys to open the chest!");
                }
            }
        }
    }
}
