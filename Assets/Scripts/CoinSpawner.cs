using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private float _horizontalVelocitySpread = 0.5f;
    [SerializeField] private float _startCoinSpeed = 3;
    [SerializeField] private int _coinsCount;

    private void Start()
    {
        SpawnCoins(_coinsCount);
    }

    public void SpawnCoins(int count)
    {
        Coin coin;
        Vector2 randomVelocityDirection;

        for (int i = 0; i < count; i++)
        {
            coin = Instantiate(_coinPrefab, transform.position, Quaternion.identity);
            randomVelocityDirection = new Vector2(Random.Range(-_horizontalVelocitySpread, _horizontalVelocitySpread), 1);
            coin.GiveStartVelocity(randomVelocityDirection * _startCoinSpeed);
        }
    }
}
