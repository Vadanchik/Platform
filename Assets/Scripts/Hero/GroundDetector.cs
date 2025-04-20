using System;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private int _enterCount = 0;

    public event Action<bool> Grounded;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
        {
            _enterCount++;

            if (_enterCount > 0)
            {
                Grounded?.Invoke(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
        {
            _enterCount--;

            if (_enterCount <= 0)
            {
                Grounded?.Invoke(false);
            }
        }
    }
}
