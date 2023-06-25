using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerfulSlice : Abilities
{
    [SerializeField] Transform whereToSpawnVFX;
    [SerializeField] GameObject sliceHit;

    float timer;
    bool canUse;
    public override void GetUse()
    {
        if (boss.mana >= manaConsumed && canUse)
        {
            anim.SetTrigger("slice01");
            boss.mana -= manaConsumed;
            timer = CD;
            canUse = false;
        }
    }

    public void Slash()
    {
        Instantiate(sliceHit, whereToSpawnVFX.position, transform.rotation);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            canUse = true;
        }

    }
}
