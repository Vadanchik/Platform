using UnityEngine;

public class HeroAnimator : MonoBehaviour
{
    [SerializeField] private HeroMovement _heroMovement;
    [SerializeField] private Jumper _jumper;
    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        _heroMovement.Running += SetSpeed;
        _jumper.Flying += SetIsGrounded;
        _jumper.Jumped += TriggerJump;
    }

    private void OnDisable()
    {
        _heroMovement.Running -= SetSpeed;
        _jumper.Flying -= SetIsGrounded;
        _jumper.Jumped -= TriggerJump;
    }

    private void SetSpeed(float velocity)
    {
        _animator.SetFloat(PlayerAnimatorData.Params.Speed, Mathf.Clamp01(Mathf.Abs(velocity)));
    }

    private void TriggerJump()
    {
        _animator.SetTrigger(PlayerAnimatorData.Params.Jump);
    }

    private void SetIsGrounded(bool isGrounded)
    {
        _animator.SetBool(PlayerAnimatorData.Params.IsGrounded, isGrounded);
    }
}
