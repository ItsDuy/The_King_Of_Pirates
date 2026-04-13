using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int id;
    [SerializeField] private enum ItemType { Potion, Coin, BlueGem, CheckPoint}
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer.value) != 0)
        {
           Debug.Log("Player collected an item!");
        }
        switch(id)
        {
            case 0:
                Debug.Log("Player collected a potion!");
                break;
            case 1:
                Debug.Log("Player collected a coin!");
                break;
            case 2:
                Debug.Log("Player collected a blue gem!");
                break;
            case 3:
                Debug.Log("Player reached a checkpoint!");
                break;
            default:
                Debug.Log("Unknown item collected.");
                break;
        }
        Destroy(gameObject);
    }
}
