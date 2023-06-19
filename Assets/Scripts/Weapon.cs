using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int damage;
    public List<IDamageable> damagedUnits = new List<IDamageable>();
    public bool isAttacking;

    public bool IsAttacking
    {
        get => isAttacking;
        set
        {
            isAttacking = value;
            if (!isAttacking && damagedUnits.Count > 0)
                damagedUnits.ForEach(x => x.HP -= damage);
            
            damagedUnits.Clear();   
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(!isAttacking)
            return;
        
        var unitToDamage = other.GetComponent<IDamageable>();

        if (unitToDamage != null)
        {
            if(!damagedUnits.Contains(unitToDamage))
                damagedUnits.Add(unitToDamage);
        }
    }
}