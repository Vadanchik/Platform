using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Flipper))]
[RequireComponent(typeof(GroundDetector))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Flipper _flipper;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private float _movementSpeed = 1.0f;
    [SerializeField] private bool _canMove = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _flipper = GetComponent<Flipper>();
        _groundDetector = GetComponent<GroundDetector>();
    }
    private void OnEnable()
    {
        _groundDetector.Grounded += SetCanMove;
    }

    private void OnDisable()
    {
        _groundDetector.Grounded -= SetCanMove;
    }
    public void SetCanMove(bool canMove) => _canMove = canMove;

    public void MoveToTarget(Vector3 targetPosition)
    {
        Debug.Log(_canMove);
        if (_canMove)
        {
            Vector2 direction = targetPosition - transform.position;
            _rigidbody.velocity = Mathf.Sign(direction.x) * Vector3.right * _movementSpeed;
            _flipper.SetLookRotation(direction.x);
        }
    }
}
