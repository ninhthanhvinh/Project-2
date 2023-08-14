using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{

    [SerializeField] private float damage;
    [SerializeField] private NormalAttack normalAttack;
    private void OnTriggerEnter(Collider other)
    {
        if (normalAttack.isAttacking && other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<IDamageable>().GetDamage(damage);
            normalAttack.isAttacking = false;
        }
    }
}
