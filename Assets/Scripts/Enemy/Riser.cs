using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class Riser : MonoBehaviour
{
    [SerializeField] private float _riseTime;
    [SerializeField] private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        _enemy.SetCanMove(false);
        Invoke(nameof(StartPatrol), _riseTime);
    }

    private void StartPatrol()
    {
        _enemy.SetCanMove(true);
    }
}