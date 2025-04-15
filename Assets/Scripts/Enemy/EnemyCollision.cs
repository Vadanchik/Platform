using System;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public event Action PatrolPointEntered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PatrolPoint>(out _))
        {
            PatrolPointEntered?.Invoke();
        }
    }
}
