using System;
using UnityEngine;

public class PlayerCore : MonoBehaviour,IDamageable
{
    [SerializeField]private UnitAnimatorController animatorController;
    
    public int maxHp;
    [SerializeField]private int currentHp;
    public int HP
    {
        get => currentHp;
        set
        {
            currentHp = Mathf.Clamp(value,0,maxHp);
            if (currentHp > 0)
            {
                OnHpDecrease?.Invoke();
                animatorController.DamageAnimation();
            }
            else
            {
                animatorController.DieAnimation();
                OnPlayerDead?.Invoke();
                StopAllCoroutines();
            }
        } 
        
    }

    public Action OnHpDecrease;
    public static Action OnPlayerDead;
    public static bool playerIsDead;

    public void OnGameStart()
    {
        currentHp = maxHp;
        playerIsDead = false;

        animatorController.OnGameStart();
    }
}