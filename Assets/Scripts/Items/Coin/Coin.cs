using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private int _value = 1;

    public int Value => _value;

    public void SetStartVelocity(Vector2 startVelocity)
    {
        _rigidbody.velocity = startVelocity;
    }
}
