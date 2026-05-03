using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LevelRespawnable))]
public class Items : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private ItemType itemType;
    [SerializeField] private int amount = 1;
    [SerializeField] private SfxManager sfxManager;
    [SerializeField] private AudioClip collectClip;
    private float destroyDelay = 0.5f;
    private Animator animator;

    private enum ItemType
    {
        Coin,
        BlueGem,
        Key
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer.value) == 0)
        {
            return;
        }

        Inventory inventory = collision.GetComponent<Inventory>();
        if (inventory == null)
        {
            inventory = collision.GetComponentInParent<Inventory>();
        }

        if (inventory == null)
        {
            return;
        }

        switch (itemType)
        {
            case ItemType.Coin:
                inventory.AddCoins(amount);
                break;
            case ItemType.BlueGem:
                inventory.AddBlueGems(amount);
                break;
            case ItemType.Key:
                inventory.AddKeys(amount);
                break;
            default:
                break;
        }
        StartCoroutine(CollectItem());
    }
    private IEnumerator CollectItem()
    {
        sfxManager.Play(collectClip);
        animator.SetTrigger("isCollected");
        yield return new WaitForSeconds(destroyDelay);
        if (itemType == ItemType.Key)
        {
            gameObject.SetActive(false);
            yield break;
        }

        Destroy(gameObject);
    }
}
