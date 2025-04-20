using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed = 0.125f;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _followTreshhold = 0.5f;

    private void LateUpdate()
    {
        Vector3 desiredPosition = _target.position + _offset;

        if (Mathf.Abs(transform.position.x - desiredPosition.x) > _followTreshhold)
        {
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
            transform.position = new Vector3(smoothedPosition.x, transform.position.y, transform.position.z);
        }
    }
}
