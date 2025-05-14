using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Ability : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _cooldownTime;
    [SerializeField] private float _damageRate;
    [SerializeField] private float _radius;
    [SerializeField] private int _damage;

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
        StartCoroutine(StartCast());
    }

    public IEnumerator StartCast()
    {
        CastStarted?.Invoke();
        StartCoroutine(StartCooldown());
        Coroutine damageCoroutine = StartCoroutine(GiveDamage());

        WaitForEndOfFrame tick = new WaitForEndOfFrame();
        float timer = _duration;

        while (timer > 0)
        {
            yield return tick;

            timer = Mathf.Clamp(timer - Time.deltaTime, 0, _duration);
            DurationTimerChanged?.Invoke(timer, _duration);
        }

        CastEnded?.Invoke();
        StopCoroutine(damageCoroutine);
    }

    private IEnumerator StartCooldown()
    {
        WaitForEndOfFrame tick = new WaitForEndOfFrame();
        float timer = _cooldownTime;
        _isReady = false;

        while(timer > 0)
        {
            yield return tick;

            timer = Mathf.Clamp(timer - Time.deltaTime, 0, _cooldownTime);
            CooldownTimerChanged?.Invoke(timer, _cooldownTime);
        }

        _isReady = true;
    }

    private IEnumerator GiveDamage()
    {
        WaitForSeconds tick = new WaitForSeconds(_damageRate);

        while (true)
        {
            Enemy nearestEnemy = FindNearestEnemy(Physics2D.OverlapCircleAll(transform.position, _radius));


            if (nearestEnemy != null)
            {
                nearestEnemy.TakeDamage(_damage);
                _health.Increase(_damage);
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
