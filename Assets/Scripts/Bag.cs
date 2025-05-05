using UnityEngine;

public class Bag : MonoBehaviour
{
    private int _coinsCount = 0;

    public void AddCoins(int amount)
    {
        _coinsCount += amount;
    }
}
