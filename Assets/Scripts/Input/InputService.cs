using System;
using UnityEngine;

public class InputService : MonoBehaviour
{
    public event Action JumpKeyPressed;

    private KeyCode JumpKey = KeyCode.Space;
    private const string HorizontalAxis = "Horizontal";

    public Vector3 GetDirection => new Vector3(Input.GetAxis(HorizontalAxis), 0, 0);

    private void Update()
    {
        if (Input.GetKeyDown(JumpKey))
        {
            JumpKeyPressed?.Invoke();
        }
    }
}
