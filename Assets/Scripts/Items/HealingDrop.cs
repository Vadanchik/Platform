using UnityEngine;

public class HealingDrop : MonoBehaviour
{
    [SerializeField] private int _healValue = 1;

    public int HealValue => _healValue;
}
