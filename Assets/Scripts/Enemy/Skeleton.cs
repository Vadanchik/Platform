using UnityEngine;

public class Skeleton : Enemy
{
    [SerializeField] private float _riseTime;

    private void Start()
    {
        _isPatrol = false;
        Invoke(nameof(StartPatrol), _riseTime);
    }

    private void FixedUpdate()
    {
        if (_isPatrol)
        {
            MoveToTarget(_patroller.GetCurrentTarget());
        }
    }

    private void StartPatrol()
    {
        _isPatrol = true;
    }
}
