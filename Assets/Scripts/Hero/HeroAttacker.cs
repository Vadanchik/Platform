using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(InputService))]
[RequireComponent(typeof(HeroAnimator))]
public class HeroAttacker : MonoBehaviour
{
    [SerializeField] private InputService _inputService;
    [SerializeField] private HeroAnimator _animator;
    [SerializeField] private AnimationEvents _animationEvents;

    [SerializeField] private int _attackDamage;
    [SerializeField] private float _pushForce;

    [Header("Attack Area")]
    [SerializeField] private Vector2 _offset;
    [SerializeField] private Vector2 _size;
    [SerializeField] private LayerMask _layerMask;

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
        IDamagable damagable = null;
        List<IDamagable> attackedObjects = Physics2D.OverlapBoxAll((Vector2)transform.position + new Vector2(_offset.x * transform.right.x, _offset.y), _size, 0, _layerMask)
            .Where(collider => collider.TryGetComponent(out damagable))
            .Select(collider => damagable)
            .ToList();

        foreach(IDamagable attackedObject in attackedObjects)
        {
            attackedObject.TakeDamage(_attackDamage);


            float pushDirection = Mathf.Sign(attackedObject.GetPosition().x - transform.position.x);
            attackedObject.Push((new Vector2(pushDirection, 1)).normalized * _pushForce);
        }
    }
}
