using System;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public event Action Attacking;
    public event Action AttackEnded;

    public void StartAttack()
    {
        Attacking?.Invoke();
    }

    public void EndAttack()
    {
        AttackEnded?.Invoke();
    }
}
