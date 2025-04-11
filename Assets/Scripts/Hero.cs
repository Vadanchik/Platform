using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private int _coinsCount = 0;

    public void TakeCoin()
    {
        _coinsCount++;
    }
}
