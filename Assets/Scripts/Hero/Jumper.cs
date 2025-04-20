using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private InputService _inputService;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private GroundDetector _collisionHandler;
    [SerializeField] private HeroAnimator _animator;

    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _isGrounded = false;

    private void OnEnable()
    {
        _inputService.JumpKeyPressed += Jump;
        _collisionHandler.Grounded += SetIsGrounded;
    }

    private void OnDisable()
    {
        _inputService.JumpKeyPressed -= Jump;
        _collisionHandler.Grounded -= SetIsGrounded;
    }

    private void Awake()
    {
        _inputService = GetComponent<InputService>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collisionHandler = GetComponent<GroundDetector>();
        _animator = GetComponent<HeroAnimator>();
    }

    private void SetIsGrounded(bool isGrounded)
    {
        _isGrounded = isGrounded;
        _animator.SetIsGrounded(_isGrounded);
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            _rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
            _animator.TriggerJump();
        }
    }
}
