using UnityEngine;
using UnityEngine.UI;

public class AbilityDurationViewer : MonoBehaviour
{
    [SerializeField] private Ability _ability;
    [SerializeField] private Image _image;
    [SerializeField] private GameObject _bar;

    private void OnEnable()
    {
        _ability.DurationTimerChanged += DisplayDuration;
        _ability.CastStarted += ShowDurationBar;
        _ability.CastEnded += HideDurationBar;
    }

    private void OnDisable()
    {
        _ability.DurationTimerChanged -= DisplayDuration;
        _ability.CastStarted -= ShowDurationBar;
        _ability.CastEnded -= HideDurationBar;
    }

    private void DisplayDuration(float timer, float time)
    {
        _image.fillAmount = Mathf.Clamp01(timer / time);
    }

    private void ShowDurationBar()
    {
        _bar.SetActive(true);
    }

    private void HideDurationBar()
    {
        _bar.SetActive(false);
    }
}
