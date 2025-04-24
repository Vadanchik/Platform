using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private List<IDamagable> _damagableObjects = new List<IDamagable>();

    public List<IDamagable> DamagableObjects => new List<IDamagable>(_damagableObjects);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            _damagableObjects.Add(damagable);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            _damagableObjects.Remove(damagable);
        }
    }
}
