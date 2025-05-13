using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
public class Riser : MonoBehaviour
{
    [SerializeField] private float _riseTime;
    [SerializeField] private EnemyMover _enemy;

    private void Awake()
    {
        _enemy = GetComponent<EnemyMover>();
    }

    private void Start()
    {
        _enemy.SetCanMove(false);
        StartCoroutine(WaitForRise(_riseTime));
    }

    private void StartPatrol()
    {
        _enemy.SetCanMove(true);
    }

    private IEnumerator WaitForRise(float time)
    {
        yield return new WaitForSeconds(time);

        StartPatrol();
    }
}