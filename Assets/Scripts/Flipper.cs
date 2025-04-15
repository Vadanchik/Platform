using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Flipper : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;

    private Quaternion _leftRotation = new Quaternion(0, 1, 0, 0);
    private Quaternion _rightRotation = new Quaternion(0, 0, 0, 1);

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetLookRotation(float directionX)
    {
        if (directionX > 0)
        {
            transform.rotation = _rightRotation;
        }
        else if (directionX < 0)
        {
            transform.rotation = _leftRotation;
        }
    }
}
