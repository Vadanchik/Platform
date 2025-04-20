using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] protected Patroller _patroller;
    [SerializeField] protected bool _isPatrol;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Flipper _flipper;
    [SerializeField] private float _movementSpeed = 1.0f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _patroller = GetComponent<Patroller>();
        _flipper = GetComponent<Flipper>();
    }

    protected void MoveToTarget(Vector3 targetPosition)
    {
        Vector2 direction = targetPosition - transform.position;
        _rigidbody.velocity = Mathf.Sign(direction.x) * Vector3.right * _movementSpeed;
        _flipper.SetLookRotation(direction.x);
    }
}
