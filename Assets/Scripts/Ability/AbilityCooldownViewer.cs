using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldownViewer : MonoBehaviour
{
    [SerializeField] private Ability _ability;
    [SerializeField] private Image _image;

    private void OnEnable()
    {
        _ability.CooldownTimerChanged += DisplayCooldown;
    }

    private void OnDisable()
    {
        _ability.CooldownTimerChanged -= DisplayCooldown;
    }

    private void DisplayCooldown(float timer, float time)
    {
        _image.fillAmount = Mathf.Clamp01(timer/time); 
    }
}
