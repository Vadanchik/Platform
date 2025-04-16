using System;
using UnityEngine;

[RequireComponent(typeof(InputService))]
[RequireComponent(typeof(Rigidbody2D))]
public class HeroMovement : MonoBehaviour
{
    [SerializeField] private InputService _inputService;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private Flipper _flipper;
    //[SerializeField] private HeroCollisionHandler _collisionHandler;

    [SerializeField] private float _movementSpeed;
    //[SerializeField] private float _jumpForce;
    //[SerializeField] private bool _isGrounded = false;

    public event Action<float> Running;
    //public event Action Jumped;
    //public event Action<bool> Flying;

    /*private void OnEnable()
    {
        _inputService.JumpKeyPressed += Jump;
        _collisionHandler.Grounded += SetIsGrounded;
    }

    private void OnDisable()
    {
        _inputService.JumpKeyPressed -= Jump;
        _collisionHandler.Grounded -= SetIsGrounded;
    }*/

    private void Awake()
    {
        _inputService = GetComponent<InputService>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _flipper = GetComponent<Flipper>();
        //_collisionHandler = GetComponent<HeroCollisionHandler>();
    }

    private void Update()
    {
        Running?.Invoke(_rigidbody.velocity.x);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_inputService.GetDirection.x * _movementSpeed, _rigidbody.velocity.y);
        _flipper.SetLookRotation(_inputService.GetDirection.x);
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
        {
            _isGrounded = true;
            Flying?.Invoke(_isGrounded);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
        {
            _isGrounded = false;
            Flying?.Invoke(_isGrounded);
        }
    }*/

    /*private void SetIsGrounded(bool isGrounded)
    {
        _isGrounded = isGrounded;
        Flying?.Invoke(_isGrounded);
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            _rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
            Jumped?.Invoke();
        }
    }*/
}
