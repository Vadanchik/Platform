using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputService))]
[RequireComponent(typeof(HeroAnimator))]
public class HeroAttacker : MonoBehaviour
{
    [SerializeField] private InputService _inputService;
    [SerializeField] private HeroAnimator _animator;
    [SerializeField] private AnimationEvents _animationEvents;
    [SerializeField] private AttackArea _attackArea;

    [SerializeField] private int _attackDamage;
    [SerializeField] private float _pushForce;

    private List<IDamagable> _currentAttackedDamagables = new List<IDamagable>();

    private void Awake()
    {
        _inputService = GetComponent<InputService>();
        _animator = GetComponent<HeroAnimator>();
    }

    private void OnEnable()
    {
        _inputService.AttackKeyPressed += StartAttack;
        _animationEvents.Attacking += Attack;
    }

    private void OnDisable()
    {
        _inputService.AttackKeyPressed -= StartAttack;
        _animationEvents.Attacking -= Attack;
    }

    private void StartAttack()
    {
        _animator.TriggerAttack();
    }

    private void Attack()
    {
        List<IDamagable> attackedObjects = _attackArea.DamagableObjects;

        foreach(IDamagable attackedObject in attackedObjects)
        {
            attackedObject.TakeDamage(_attackDamage);


            float pushDirection = Mathf.Sign(attackedObject.GetPosition().x - transform.position.x);
            attackedObject.Push((new Vector2(pushDirection, 1)).normalized * _pushForce);
        }
    }
}
