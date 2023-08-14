using BehaviorDesigner.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Creep : MonoBehaviour, IDamageable
{
    [Tooltip("Maximum health that character has.")]
    public float maxHealth;
    [Tooltip("Score gained if u killed this.")]
    public int gainScore;
    public Transform player;
    public float speed = 5;

    public Slider healthSlider;

    [HideInInspector]
    public float currentHealth;
    NavMeshAgent agent;
    Animator anim;
    BehaviorTree tree;
    Rigidbody rb;
    AudioManager audioManager;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        tree = GetComponent<BehaviorTree>();
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        audioManager = AudioManager.Instance;
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        agent.speed = speed;

        if (rb.velocity.magnitude > 0f)
        {
            anim.SetBool("isMoving", true);
            audioManager.PlaySound("BoneDoSmth");
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        healthSlider.value = currentHealth / maxHealth;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void GetDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            gameManager.score += gainScore;
            gameManager.kill++;
            Die();
        }
    }

    public void GetHeal(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
