using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ItemPicker))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Bag))]
public class Hero : MonoBehaviour, IDamagable
{
    [SerializeField] private ItemPicker _itemPicker;
    [SerializeField] private float _immunityTime = 1;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Health _health;
    [SerializeField] private Bag _bag;

    private bool _hasImmunity = false;

    private void Awake()
    {
        _itemPicker = GetComponent<ItemPicker>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _health = GetComponent<Health>();
        _bag = GetComponent<Bag>();
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

    public void TakeCoin(int amount)
    {
        _bag.AddCoins(amount);
    }

    public void TakeDamage(int damage)
    {
        if (_hasImmunity == false & damage > 0)
        {
            _health.SubstractHealth(damage);
            StartCoroutine(StartImmunityTimer(_immunityTime));

            if (_health.IsAlive == false)
            {
                Die();
            }
        }
    }

    public void Push(Vector2 impulse)
    {
        if (_hasImmunity == false)
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(impulse);
        }
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
        if (_health.CurrentHealth < _health.MaxHealth)
        {
            _health.IncreaseHealth(healingDrop.HealValue);
            Destroy(healingDrop.gameObject);
        }
    }

    private IEnumerator StartImmunityTimer(float time)
    {
        WaitForSeconds waitingTime = new WaitForSeconds(time);
        _hasImmunity = true;

        yield return waitingTime;

        _hasImmunity = false;
    }
}
