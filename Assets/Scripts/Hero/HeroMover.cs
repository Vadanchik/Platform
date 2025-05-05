using UnityEngine;

[RequireComponent(typeof(InputService))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HeroAnimator))]
[RequireComponent(typeof(Flipper))]
public class HeroMover : MonoBehaviour
{
    [SerializeField] private InputService _inputService;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private HeroAnimator _animator;
    [SerializeField] private Flipper _flipper;

    [SerializeField] private float _movementSpeed;

    private void Awake()
    {
        _inputService = GetComponent<InputService>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _flipper = GetComponent<Flipper>();
        _animator = GetComponent<HeroAnimator>();
    }

    private void Update()
    {
        _animator.SetSpeed(_rigidbody.velocity.x);
    }

    private void FixedUpdate()
    {
        if (_inputService.Direction.x != 0)
        {
            Move();
            _flipper.SetLookRotation(_inputService.Direction.x);
        }
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector2(_inputService.Direction.x * _movementSpeed, _rigidbody.velocity.y);
    }
}
