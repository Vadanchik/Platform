using UnityEngine;

public interface IDamagable
{
    public void TakeDamage(int damage);
    public void Push(Vector2 impulse);
    public void Die();
    public Vector2 GetPosition();
}
