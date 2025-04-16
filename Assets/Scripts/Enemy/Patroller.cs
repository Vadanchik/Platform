using UnityEngine;

public class Patroller : MonoBehaviour
{
    [SerializeField] private PatrolPoint[] _patrolPoints;
    [SerializeField] private EnemyCollisionHandler _enemyCollision;

    private int _currentPatrolPointIndex = 0;

    private void Awake()
    {
        _enemyCollision = GetComponent<EnemyCollisionHandler>();
    }

    private void OnEnable()
    {
        _enemyCollision.PatrolPointEntered += ChangePatrolDirection;
    }

    private void OnDisable()
    {
        _enemyCollision.PatrolPointEntered -= ChangePatrolDirection;
    }

    private void ChangePatrolDirection()
    {
        _currentPatrolPointIndex = (_currentPatrolPointIndex + 1) % _patrolPoints.Length;
    }

    public Vector3 GetCurrentTarget()
    {
        return _patrolPoints[_currentPatrolPointIndex].Position;
    }
}
