using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{
    [SerializeField]
    private ScriptableObjectIntVariable _playerCoinCounter;
    void Awake()
    {
        //_playerCoinCounter.RestoreValue(); //��� ���������
        _playerCoinCounter.ChangeVariable(0);
    }
}
