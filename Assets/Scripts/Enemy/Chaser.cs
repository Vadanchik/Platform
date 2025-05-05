using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
public class Chaser : MonoBehaviour
{
    [SerializeField] private float _detectRadius = 2.0f;
    [SerializeField] private LayerMask _layersToDetect;
    [SerializeField] private EnemyMover _mover;

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
    }
    public void Chase(Vector3 targetPosition)
    {
        _mover.MoveToTarget(targetPosition);
    }

    public bool TryFindHeroInRange(out Hero hero)
    {
        hero = FindHeroInRange();

        if (hero != null)
        {
            return true;
        }

        return false;
    }

    private Hero FindHeroInRange()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _detectRadius, _layersToDetect);

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.TryGetComponent<Hero>(out Hero hero))
            {
                return hero;
            }
        }

        return null;
    }
}
