using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTriggerDeath : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().GetDamage(1000);
        }
    }
}
