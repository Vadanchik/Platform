using System;
using UnityEngine;

public class InputService : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    
    private KeyCode _jumpKey = KeyCode.Space;

    public event Action JumpKeyPressed;

    public Vector3 Direction => new Vector3(Input.GetAxis(HorizontalAxis), 0, 0);

    private void Update()
    {
        if (Input.GetKeyDown(_jumpKey))
        {
            JumpKeyPressed?.Invoke();
        }
    }
}
