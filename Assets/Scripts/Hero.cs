using UnityEngine;

[RequireComponent(typeof(ItemPicker))]
public class Hero : MonoBehaviour
{
    [SerializeField] private ItemPicker _itemPicker;

    private int _coinsCount = 0;

    private void Awake()
    {
        _itemPicker = GetComponent<ItemPicker>();
    }

    private void OnEnable()
    {
        _itemPicker.CoinPicked += TakeCoin;
    }

    private void OnDisable()
    {
        _itemPicker.CoinPicked -= TakeCoin;
    }

    public void TakeCoin()
    {
        _coinsCount++;
    }
}
