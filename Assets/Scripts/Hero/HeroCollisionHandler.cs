using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCollisionHandler : MonoBehaviour
{
    public event Action<bool> Grounded;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
        {
            Grounded?.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
        {
            Grounded?.Invoke(false);
        }
    }
}
