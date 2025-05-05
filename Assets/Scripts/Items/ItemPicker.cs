using System;
using UnityEngine;

[RequireComponent(typeof(Hero))]
public class ItemPicker : MonoBehaviour
{
    public event Action<int> CoinPicked;
    public event Action<HealingDrop> HealingDropPicked;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            CoinPicked?.Invoke(coin.Value);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.TryGetComponent(out HealingDrop healingDrop))
        {
            HealingDropPicked?.Invoke(healingDrop);
        }
    }
}
