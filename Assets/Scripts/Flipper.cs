using UnityEngine;

public class Flipper : MonoBehaviour
{
    [SerializeField] private Transform _spriteTransform;

    private Quaternion _leftRotation = new Quaternion(0, 1, 0, 0);
    private Quaternion _rightRotation = new Quaternion(0, 0, 0, 1);

    public void SetLookRotation(float directionX)
    {
        if (directionX > 0)
        {
            _spriteTransform.rotation = _rightRotation;
        }
        else if (directionX < 0)
        {
            _spriteTransform.rotation = _leftRotation;
        }
    }
}
