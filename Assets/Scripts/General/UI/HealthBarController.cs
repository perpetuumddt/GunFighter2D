using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField]
    private Image[] _heartImg;

    public void UpdateHealthBar(int _currentHealth)
    {
        for (int i = 0; i < _heartImg.Length; i++)
        {
            if (i < _currentHealth)
            {
                _heartImg[i].enabled = true;
            }
            else
            {
                _heartImg[i].enabled = false;
            }
        }
    }
}
