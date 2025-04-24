using UnityEngine;

public class Chaser : MonoBehaviour
{
    [SerializeField] private float _detectRadius = 2.0f;
    [SerializeField] private LayerMask _layersToDetect;

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
