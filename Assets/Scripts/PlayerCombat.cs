using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerCombat : Combat
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Attack();
    }
}
