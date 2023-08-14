using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obelisk : MonoBehaviour, IDamageable
{
    public float health;
    private float maxHealth;

    public float timeBetweenSpawn;

    public Transform spawnPoint;
    public GameObject spawnObject;
    public int scoreGain;

    public GameObject boss;
    public Slider slider;

    private AudioManager audioManager;
    public ParticleSystem collapseVFX;

    public bool isFallen = false;

    private void Start()
    {
        audioManager = AudioManager.Instance;
        maxHealth = health;
    }
    public void GetDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            //Do something
            audioManager.PlaySound("ObeliskDestroy");
            isFallen = true;
            collapseVFX.Play();
            GameManager.Instance.score += scoreGain;
            Invoke("InstantiateBoss", 2f);
        }
    }

    public void GetHeal(float healAmount)
    {
        //Obelisk can't be healed
        throw new System.NotImplementedException();
    }

    private void Update()
    {
        if (Time.time > timeBetweenSpawn && !isFallen)
        {
            timeBetweenSpawn = Time.time + timeBetweenSpawn;
            //Spawn something
            Instantiate(spawnObject, spawnPoint.position, Quaternion.identity);
        }

        if (isFallen)
        {
            gameObject.isStatic = false;
            transform.position -= Vector3.up * 9.8f * Time.deltaTime * 2;
        }

        slider.value = health / maxHealth;
    }

    private void InstantiateBoss()
    {
        boss.SetActive(true);
    }
}
