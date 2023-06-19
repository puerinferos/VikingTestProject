using System;
using UnityEngine;
using UnityEngine.UI;

public class InGameWindow : UIWindow
{
    [SerializeField]private PlayerCore player;
    [SerializeField] private Image filledHPBar;

    private void Awake()
    {
        player.OnHpDecrease += OnHpDecrease;
    }

    private void OnHpDecrease()
    {
        filledHPBar.fillAmount = (float)((float)player.HP / (float)player.maxHp);        
    }
}