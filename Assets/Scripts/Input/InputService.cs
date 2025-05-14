using System;
using UnityEngine;

public class InputService : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    
    private KeyCode _jumpKey = KeyCode.Space;
    private KeyCode _abilityKey = KeyCode.F;

    public event Action JumpKeyPressed;
    public event Action AttackKeyPressed;
    public event Action AbilityKeyPressed;

    public Vector3 Direction => new Vector3(Input.GetAxis(HorizontalAxis), 0, 0);

    private void Update()
    {
        if (Input.GetKeyDown(_jumpKey))
        {
            JumpKeyPressed?.Invoke();
        }

        if (Input.GetMouseButtonDown(0))
        {
            AttackKeyPressed?.Invoke();
        }

        if (Input.GetKeyDown(_abilityKey))
        {
            AbilityKeyPressed?.Invoke();
        }
    }
}
