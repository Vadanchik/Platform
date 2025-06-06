using System;
using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Ability : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _cooldownTime;
    [SerializeField] private float _damageInterval;
    [SerializeField] private float _radius;
    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _enemyLayer;

    private Health _health;
    private bool _isReady = true;

    public event Action<float, float> DurationTimerChanged;
    public event Action<float, float> CooldownTimerChanged;
    public event Action CastStarted;
    public event Action CastEnded;

    public bool IsReady => _isReady;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    public void ApplyCast()
    {
        StartCoroutine(ExecuteCast());
    }

    private IEnumerator ExecuteCast()
    {
        CastStarted?.Invoke();
        Coroutine damageCoroutine = StartCoroutine(ExecuteDamage());

        WaitForEndOfFrame tick = new WaitForEndOfFrame();
        float timer = 0;

        while (timer < _duration)
        {
            yield return tick;

            timer = Mathf.Clamp(timer + Time.deltaTime, 0, _duration);
            DurationTimerChanged?.Invoke(timer, _duration);
        }
        
        _isReady = false;
        StopCoroutine(damageCoroutine);
        StartCoroutine(ExecuteCooldown());
    }

    private IEnumerator ExecuteCooldown()
    {
        WaitForEndOfFrame tick = new WaitForEndOfFrame();
        float timer = _cooldownTime;

        while(timer > 0)
        {
            yield return tick;

            timer = Mathf.Clamp(timer - Time.deltaTime, 0, _cooldownTime);
            DurationTimerChanged?.Invoke(timer, _cooldownTime);
        }

        CastEnded?.Invoke();
        _isReady = true;
    }

    private IEnumerator ExecuteDamage()
    {
        WaitForSeconds tick = new WaitForSeconds(_damageInterval);
        int enemyCount = 2;

        while (enabled)
        {
            Collider2D[] colliders = new Collider2D[enemyCount];
            int count = Physics2D.OverlapCircleNonAlloc(transform.position, _radius, colliders, _enemyLayer);

            Enemy nearestEnemy = FindNearestEnemy(colliders.Take(count).ToArray());

            if (nearestEnemy != null)
            {
                _health.Increase(nearestEnemy.TakeDamage(_damage));
            }

            yield return tick;
        }
    }

    private Enemy FindNearestEnemy(Collider2D[] colliders)
    {
        float minDistance = float.MaxValue;
        Enemy nearestEnemy = null;

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out Enemy enemy) && (enemy.transform.position - transform.position).magnitude < minDistance)
            {
                minDistance = (enemy.transform.position - transform.position).magnitude;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }
}
