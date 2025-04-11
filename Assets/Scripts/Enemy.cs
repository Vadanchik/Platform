using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected bool _isPatrol;
    [SerializeField] private float _movementSpeed = 1.0f;
    [SerializeField] private PatrolPoint[] _patrolPoints;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private int _currentPatrolPointIndex = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PatrolPoint>(out _))
        {
            ChangePatrolDirection();
        }
    }

    protected void PatrolMove()
    {
        MoveToTarget(_patrolPoints[_currentPatrolPointIndex].Position);
    }

    protected void ChangePatrolDirection()
    {
        _currentPatrolPointIndex = (_currentPatrolPointIndex + 1) % _patrolPoints.Length;
    }

    private void MoveToTarget(Vector3 targetPosition)
    {
        Vector2 direction = targetPosition - transform.position;
        transform.Translate(new Vector3(direction.x, 0, 0).normalized * _movementSpeed * Time.deltaTime);

        if (direction.x < 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (direction.x > 0)
        {
            _spriteRenderer.flipX = true;
        }
    }
}
