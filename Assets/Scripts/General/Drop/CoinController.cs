using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class CoinController : MonoBehaviour, ICollectable
{
    [SerializeField]
    private ScriptableObjectIntVariable _playerCoinCounter; 

    [SerializeField]
    private ScriptableObjectIntVariable _coinAmount; //value of 1 coin = 1

    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    public async void DoCollect()
    {
        _playerCoinCounter.ChangeVariable(_playerCoinCounter.Variable+_coinAmount.Variable);
        //Sprite renderer set active false
        //await Task.Delay(1000);
        //Play PS
        //await Task.Delay(1000);
        Destroy(gameObject);
    }
}
