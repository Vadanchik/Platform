using System;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private InputService _inputService;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private HeroCollisionHandler _collisionHandler;

    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _isGrounded = false;

    public event Action Jumped;
    public event Action<bool> Flying;

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
        _collisionHandler = GetComponent<HeroCollisionHandler>();
    }

    private void SetIsGrounded(bool isGrounded)
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
    }
}
