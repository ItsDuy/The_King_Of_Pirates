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

        }
        switch(id)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
        Destroy(gameObject);
    }
}
