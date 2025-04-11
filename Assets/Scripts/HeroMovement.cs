using UnityEngine;

[RequireComponent(typeof(InputService))]
[RequireComponent(typeof(Rigidbody2D))]
public class HeroMovement : MonoBehaviour
{
    [SerializeField] private InputService _inputService;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;

    [SerializeField] private bool _isGrounded = true;

    private void OnEnable()
    {
        _inputService.OnJumpKeyDown += Jump;
    }

    private void OnDisable()
    {
        _inputService.OnJumpKeyDown -= Jump;
    }

    private void Awake()
    {
        _inputService = GetComponent<InputService>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _animator.SetFloat(PlayerAnimatorData.Params.Speed, Mathf.Clamp01(Mathf.Abs(_rigidbody.velocity.x)));
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
        {
            _isGrounded = true;
            _animator.SetBool(PlayerAnimatorData.Params.IsGrounded, _isGrounded);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
        {
            _isGrounded = false;
            _animator.SetBool(PlayerAnimatorData.Params.IsGrounded, _isGrounded);
        }
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector2(_inputService.GetDirection.x * _movementSpeed, _rigidbody.velocity.y);

        if (_rigidbody.velocity.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_rigidbody.velocity.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }

    private void Jump()
    {
        if (_isGrounded == true)
        {
            _rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
            _animator.SetTrigger(PlayerAnimatorData.Params.Jump);
        }
    }
}
