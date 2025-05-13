using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthViewer : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Image _healthSmoothBar;

    [SerializeField] private float _speed = 1.0f;

    private Coroutine _currentCoroutine;

    private void OnEnable()
    {
        _health.ValueChanged += DisplayView;
        DisplayView(_health.CurrentValue);
    }

    private void OnDisable()
    {
        _health.ValueChanged -= DisplayView;
    }

    private void DisplayView(float value)
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = StartCoroutine(DisplaySmoothBar(value));
    }

    private IEnumerator DisplaySmoothBar(float targetValue)
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        while (Mathf.Approximately(_healthSmoothBar.fillAmount, targetValue) == false)
        {
            _healthSmoothBar.fillAmount = Mathf.MoveTowards(_healthSmoothBar.fillAmount, targetValue / _health.MaxValue, Time.deltaTime * _speed);
            yield return wait;
        }
    }
}
