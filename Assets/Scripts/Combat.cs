using System;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] protected Weapon weapon;
    [SerializeField]protected int maxCombo = 3;
    [SerializeField]protected float attackTime = .5f;
    protected Animator animator;
    protected int currentCombo;
    protected static readonly int Combos = Animator.StringToHash("Combos");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name.Contains("Combo"))
            {
                
                AnimationEvent onAttackEnd = new AnimationEvent();
                AnimationEvent onAttackStart = new AnimationEvent();
                onAttackEnd.time = attackTime;
                onAttackStart.time = 0f;

                onAttackStart.functionName = nameof(OnAttackStart);
                onAttackEnd.functionName = nameof(OnAttackEnd);

                clip.AddEvent(onAttackStart);
                clip.AddEvent(onAttackEnd);
            }
        }
    }

    private void OnAttackStart()
    {
        if (weapon != null)
            weapon.IsAttacking = true;
    }

    private void OnAttackEnd()
    {
        if (weapon != null)
            weapon.IsAttacking = false;
    }

    public void OnComboAnimationEnd()
    {
        currentCombo = 0;
        animator.SetInteger(Combos,currentCombo);
    }

    public void Attack()
    {
        if(PlayerCore.playerIsDead)
            return;
        currentCombo++;
        currentCombo = Mathf.Clamp(currentCombo, 0, maxCombo);
        animator.SetInteger(Combos,currentCombo);
    }
}