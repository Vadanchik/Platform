using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputService : MonoBehaviour
{
    public event Action OnJumpKeyDown;

    private const string HorizontalAxis = "Horizontal";

    public Vector3 GetDirection => new Vector3(Input.GetAxis(HorizontalAxis), 0, 0);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpKeyDown?.Invoke();
        }
    }
}
