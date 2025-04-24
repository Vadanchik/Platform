using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ItemPicker))]
public class Hero : MonoBehaviour, IDamagable
{
    [SerializeField] private ItemPicker _itemPicker;
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private float _immunityTime = 1;

    private bool _hasImmunity = false;
    private int _currentHealth;
    private int _coinsCount = 0;

    private void Awake()
    {
        _itemPicker = GetComponent<ItemPicker>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void OnEnable()
    {
        _itemPicker.CoinPicked += TakeCoin;
        _itemPicker.HealingDropPicked += Heal;

    }

    private void OnDisable()
    {
        _itemPicker.CoinPicked -= TakeCoin;
        _itemPicker.HealingDropPicked -= Heal;
    }

    public void TakeCoin()
    {
        _coinsCount++;
    }

    public void TakeDamage(int damage)
    {
        if (_hasImmunity == false)
        {
            ChangeHealth(-damage);
            StartCoroutine(StartImmunityTimer(_immunityTime));

            if (_currentHealth == 0)
            {
                Die();
            }
        }
    }

    public void Push(Vector2 impulse)
    {

    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    private void Heal(HealingDrop healingDrop)
    {
        if (_currentHealth < _maxHealth)
        {
            ChangeHealth(healingDrop.HealValue);
            Destroy(healingDrop.gameObject);
        }
    }

    private void ChangeHealth(int value)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + value, 0, _maxHealth);
    }

    private IEnumerator StartImmunityTimer(float time)
    {
        WaitForSeconds waitingTime = new WaitForSeconds(time);
        _hasImmunity = true;

        yield return waitingTime;

        _hasImmunity = false;
    }
}
