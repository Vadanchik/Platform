using UnityEngine;

public interface IDamagable
{
    public int TakeDamage(int damage);
    public void Push(Vector2 impulse);
    public void Die();
    public Vector2 GetPosition();
}
