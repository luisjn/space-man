using System;
using Enums;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : MonoBehaviour
{
    private PlayerController _playerController;
    private Slider _slider;

    public BarType type;
    
    private void Start()
    {
        _slider = GetComponent<Slider>();
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        switch (type)
        {
            case BarType.HealthBar:
                _slider.value = PlayerController.MaxHealth;
                break;
            case BarType.ManaBar:
                _slider.value = PlayerController.MaxMana;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Update()
    {
        switch (type)
        {
            case BarType.HealthBar:
                _slider.value = _playerController.GetHealth();
                break;
            case BarType.ManaBar:
                _slider.value = _playerController.GetMana();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
