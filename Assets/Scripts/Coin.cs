using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Hero>(out Hero hero))
        {
            hero.TakeCoin();
            Destroy(gameObject);
        }
    }

    public void GiveStartVelocity(Vector2 startVelocity)
    {
        _rigidbody.velocity = startVelocity;
    }
}
