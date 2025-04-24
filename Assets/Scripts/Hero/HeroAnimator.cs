using UnityEngine;

public class HeroAnimator : MonoBehaviour
{
    [SerializeField] private HeroMover _heroMovement;
    [SerializeField] private Jumper _jumper;
    [SerializeField] private Animator _animator;

    public void SetSpeed(float velocity)
    {
        _animator.SetFloat(PlayerAnimatorData.Params.Speed, Mathf.Clamp01(Mathf.Abs(velocity)));
    }

    public void TriggerJump()
    {
        _animator.SetTrigger(PlayerAnimatorData.Params.Jump);
    }

    public void TriggerAttack()
    {
        _animator.SetTrigger(PlayerAnimatorData.Params.Attack);
    }

    public void SetIsGrounded(bool isGrounded)
    {
        _animator.SetBool(PlayerAnimatorData.Params.IsGrounded, isGrounded);
    }
}
