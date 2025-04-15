using System;
using UnityEngine;

[RequireComponent(typeof(Hero))]
public class ItemPicker : MonoBehaviour
{
    public event Action CoinPicked;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Coin>(out _))
        {
            CoinPicked?.Invoke();
            Destroy(collision.gameObject);
        }
    }
}
