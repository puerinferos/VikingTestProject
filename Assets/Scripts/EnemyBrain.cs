using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyBrain : MonoBehaviour,IDamageable
{
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private Combat combat;
    private UnitAnimatorController animatorController;
    [SerializeField] private float distanceToAttack;
    [SerializeField] private float attacksCoolDown;
    
    private Transform playerTarget;

    private float DistanceToTarget => Vector3.Distance(transform.position, playerTarget.position);

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
                animatorController.DamageAnimation();
            }
            else
            {
                animatorController.DieAnimation();
                OnDead?.Invoke(this);
                StopAllCoroutines();
            }
        } 
        
    }
    public static Action<EnemyBrain> OnDead;

    private void Awake()
    {
        animatorController = GetComponent<UnitAnimatorController>();
    }

    public void Reset()
    {
        currentHp = maxHp;
        StartCoroutine(MovingCoroutine());
    }

    public void Initialise(Transform player,int startHP)
    {
        maxHp = startHP;
        playerTarget = player;
        
        currentHp = maxHp;

        StartCoroutine(MovingCoroutine());
    }

    private IEnumerator MovingCoroutine()
    {
        while (DistanceToTarget > distanceToAttack)
        {
            enemyMovement.movementInput = (playerTarget.position - transform.position).normalized;
            enemyMovement.Rotate();
            enemyMovement.Move();
            yield return new WaitForFixedUpdate();
        }
        enemyMovement.movementInput = Vector3.zero;
        while (enemyMovement.CurrentSpeed == 0)
        {
            enemyMovement.Move();
            yield return null;
        }

        StartCoroutine(AttackingCoroutine());
    }
    private IEnumerator AttackingCoroutine()
    {
        while (DistanceToTarget <= distanceToAttack)
        {
            combat.Attack();
            
            yield return new WaitForSeconds(attacksCoolDown);
        }
        
        StartCoroutine(MovingCoroutine());
    }
}