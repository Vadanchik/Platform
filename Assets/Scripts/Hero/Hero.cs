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

    public int TakeDamage(int damage)
    {
        int takenDamage;

        if (_hasImmunity == false & damage > 0)
        {
            takenDamage = _health.Substract(damage);
            StartCoroutine(ExecuteImmunity(_immunityTime));

            if (_health.IsAlive == false)
            {
                Die();
            }
        }
        else
            takenDamage = 0;

        return takenDamage;
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
        if (_health.CurrentValue < _health.MaxValue)
        {
            _health.Increase(healingDrop.HealValue);
            Destroy(healingDrop.gameObject);
        }
    }

    private IEnumerator ExecuteImmunity(float time)
    {
        WaitForSeconds waitingTime = new WaitForSeconds(time);
        _hasImmunity = true;

        yield return waitingTime;

        _hasImmunity = false;
    }
}
