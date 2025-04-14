using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;

    public void GiveStartVelocity(Vector2 startVelocity)
    {
        _rigidbody.velocity = startVelocity;
    }
}
