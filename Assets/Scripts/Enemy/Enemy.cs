using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyCollisionHandler))]
[RequireComponent(typeof(Patroller))]
[RequireComponent(typeof(Chaser))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour, IDamagable
{
    
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private EnemyCollisionHandler _collisionHandler; 
    [SerializeField] private EnemyMover _mover;
    [SerializeField] private Chaser _chaser;
    [SerializeField] private Patroller _patroller;
    [SerializeField] private Health _health;

    [SerializeField] private int _damage = 1;
    [SerializeField] private float _pushForce = 150;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collisionHandler = GetComponent<EnemyCollisionHandler>();
        _chaser = GetComponent<Chaser>();
        _patroller = GetComponent<Patroller>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _collisionHandler.HeroTouched += Attack;
    }

    private void OnDisable()
    {
        _collisionHandler.HeroTouched -= Attack;
    }

    private void FixedUpdate()
    {
        if (_chaser.TryFindHeroInRange(out Hero hero))
        {
            _chaser.Chase(hero.transform.position);
        }
        else
        {
            _patroller.Patrol();
        }
    }

    public int TakeDamage(int damage)
    {
        int takenDamage = _health.Substract(damage);

        if (_health.IsAlive == false)
        {
            Die();
        }

        return takenDamage;
    }

    public void Push(Vector2 impulse)
    {
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(impulse);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    private void Attack(IDamagable attackTarget)
    {
        float pushDirection = Mathf.Sign(attackTarget.GetPosition().x - transform.position.x);
        attackTarget.Push((new Vector2(pushDirection, 1)).normalized * _pushForce);
        attackTarget.TakeDamage(_damage);
    }
}
