using System;
using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    public event Action PatrolPointEntered;
    public event Action<IDamagable> HeroTouched;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PatrolPoint>(out _))
        {
            PatrolPointEntered?.Invoke();
        }

        if (collision.gameObject.TryGetComponent(out Hero hero))
        {
            HeroTouched?.Invoke(hero);
        }
    }
}
