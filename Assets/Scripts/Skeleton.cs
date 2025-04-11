using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    [SerializeField] private float _riseTime;
    private void Start()
    {
        _isPatrol = false;
        Invoke(nameof(StartPatrol), _riseTime);
    }

    private void Update()
    {
        if (_isPatrol)
        {
            PatrolMove();
        }
    }

    private void StartPatrol()
    {
        _isPatrol = true;
    }
}
