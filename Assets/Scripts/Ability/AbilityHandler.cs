using UnityEngine;

[RequireComponent(typeof(InputService))]
public class AbilityHandler : MonoBehaviour
{
    [SerializeField] private InputService _input;
    [SerializeField] private Ability _ability;

    private void Awake()
    {
        _input = GetComponent<InputService>();
    }

    private void OnEnable()
    {
        _input.AbilityKeyPressed += OnClickAbilityButton;
    }

    private void OnDisable()
    {
        _input.AbilityKeyPressed -= OnClickAbilityButton;
    }

    private void OnClickAbilityButton()
    {
        if (_ability.IsReady)
        {
            _ability.ApplyCast();
        }
    }
}
