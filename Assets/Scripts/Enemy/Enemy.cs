using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Patroller))]
[RequireComponent(typeof(Chaser))]
[RequireComponent(typeof(Flipper))]
[RequireComponent(typeof(GroundDetector))]
[RequireComponent(typeof(EnemyCollisionHandler))]
public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private Patroller _patroller;
    [SerializeField] private bool _isPatrol;
    [SerializeField] private Chaser _chaser;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Flipper _flipper;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private EnemyCollisionHandler _collisionHandler;
    [SerializeField] private float _movementSpeed = 1.0f;

    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _pushForce = 150;

    private int _currentHealth;
    private bool _canMove = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _patroller = GetComponent<Patroller>();
        _flipper = GetComponent<Flipper>();
        _chaser = GetComponent<Chaser>();
        _groundDetector = GetComponent<GroundDetector>();
        _collisionHandler = GetComponent<EnemyCollisionHandler>();
    }

    private void OnEnable()
    {
        _groundDetector.Grounded += SetCanMove;
        _collisionHandler.HeroTouched += Attack;
    }

    private void OnDisable()
    {
        _groundDetector.Grounded -= SetCanMove;
        _collisionHandler.HeroTouched -= Attack;
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        _isPatrol = true;
    }

    private void FixedUpdate()
    {
        if (_chaser.TryFindHeroInRange(out Hero hero))
        {
            MoveToTarget(hero.transform.position);
        }
        else if (_isPatrol)
        {
            MoveToTarget(_patroller.GetCurrentTarget());
        }
    }

    public void SetCanMove(bool canMove) => _canMove = canMove;

    public void TakeDamage(int damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);

        if (_currentHealth == 0)
        {
            Die();
        }
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

    private void MoveToTarget(Vector3 targetPosition)
    {
        if (_canMove)
        {
            Vector2 direction = targetPosition - transform.position;
            _rigidbody.velocity = Mathf.Sign(direction.x) * Vector3.right * _movementSpeed;
            _flipper.SetLookRotation(direction.x);
        }
    }

    private void Attack(IDamagable attackTarget)
    {
        attackTarget.TakeDamage(_damage);
        float pushDirection = Mathf.Sign(attackTarget.GetPosition().x - transform.position.x);
        attackTarget.Push((new Vector2(pushDirection, 1)).normalized * _pushForce);
    }
}
