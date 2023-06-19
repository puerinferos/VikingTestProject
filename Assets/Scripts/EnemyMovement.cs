using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    public override void Move()
    {
        base.Move();
        controller.Move(targetDirection * (speed * Time.fixedDeltaTime));
    }

    public override void Rotate()
    {
        base.Rotate();
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10);
    }
}