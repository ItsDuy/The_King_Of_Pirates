using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRespawnable : MonoBehaviour
{
    [SerializeField] private bool resetAnimator = true;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;
    private bool initialActiveSelf;
    private Rigidbody2D rb2D;
    private Animator anim;
    private bool initialized;

    private void Awake()
    {
        CacheInitialState();
    }

    private void CacheInitialState()
    {
        if (initialized)
        {
            return;
        }

        initialized = true;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;
        initialActiveSelf = gameObject.activeSelf;
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void Respawn()
    {
        if (!initialized)
        {
            CacheInitialState();
        }

        bool shouldBeActive = initialActiveSelf;

        transform.position = initialPosition;
        transform.rotation = initialRotation;
        transform.localScale = initialScale;

        if (rb2D != null)
        {
            rb2D.velocity = Vector2.zero;
            rb2D.angularVelocity = 0f;
        }

        if (shouldBeActive && !gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        if (resetAnimator && anim != null)
        {
            anim.Rebind();
            if (gameObject.activeInHierarchy)
            {
                anim.Update(0f);
            }
        }

        IRespawnResettable[] resettables = GetComponents<IRespawnResettable>();
        for (int i = 0; i < resettables.Length; i++)
        {
            resettables[i].ResetState();
        }

        if (!shouldBeActive && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
