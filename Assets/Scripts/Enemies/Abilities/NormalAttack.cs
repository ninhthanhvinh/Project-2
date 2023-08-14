using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : Abilities
{

    protected float timer;
    protected bool canUse;
    public bool isAttacking = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public override void GetUse()
    {
        anim.Play("Attack1 1");
        isAttacking = true;
    }
}
