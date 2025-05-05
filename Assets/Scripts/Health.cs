using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    private int _currentHeath;

    public int CurrentHealth => _currentHeath;
    public int MaxHealth => _maxHealth;
    public bool IsAlive => _currentHeath > 0;

    private void Start()
    {
        _currentHeath = _maxHealth;
    }

    public void SubstractHealth(int value)
    {
        _currentHeath = Mathf.Clamp(_currentHeath - Mathf.Clamp(value, 0, int.MaxValue), 0, _maxHealth);
    }

    public void IncreaseHealth(int value)
    {
        _currentHeath = Mathf.Clamp(_currentHeath + Mathf.Clamp(value, 0, int.MaxValue), 0, _maxHealth);
    }
}
