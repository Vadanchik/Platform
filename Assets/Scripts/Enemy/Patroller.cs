using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
public class Patroller : MonoBehaviour
{
    [SerializeField] private PatrolPoint[] _patrolPoints;
    [SerializeField] private EnemyCollisionHandler _enemyCollision;
    [SerializeField] private EnemyMover _mover;
    [SerializeField] private bool _isPatrol = true;

    private int _currentPatrolPointIndex = 0;

    private void Awake()
    {
        _enemyCollision = GetComponent<EnemyCollisionHandler>();
        _mover = GetComponent<EnemyMover>();
    }

    private void OnEnable()
    {
        _enemyCollision.PatrolPointEntered += ChangePatrolDirection;
    }

    private void OnDisable()
    {
        _enemyCollision.PatrolPointEntered -= ChangePatrolDirection;
    }

    public void Patrol()
    {
        if (_isPatrol)
        {
            _mover.MoveToTarget(GetCurrentTarget());
        }
    }

    public Vector3 GetCurrentTarget()
    {
        return _patrolPoints[_currentPatrolPointIndex].Position;
    }

    private void ChangePatrolDirection()
    {
        _currentPatrolPointIndex = (_currentPatrolPointIndex + 1) % _patrolPoints.Length;
    }
}
