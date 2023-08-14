using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss : MonoBehaviour, IDamageable
{
    [Tooltip("Maximum health that character has.")]
    public float maxHealth;
    public Transform player;
    public float speed = 5;
    [Tooltip("Maximum mana that character has.")]
    public float maxMana;
    [HideInInspector]
    public float mana;
    [Tooltip("Time that mana will auto regen")]
    public float cdManaReg;
    float cdManaReg_timer;
    [Tooltip("Amount mana will auto regen")]
    public float manaReg_amount;
    [Tooltip("Score gained if u killed this.")]
    public int gainScore;

    public Slider healthSlider;
    public Slider manaSlider;

    [HideInInspector]
    public float currentHealth;
    NavMeshAgent agent;
    Animator anim;
    Vector2 smoothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;
    BehaviorTree tree;
    Rigidbody rb;
    GameManager gameManager;
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        tree = GetComponent<BehaviorTree>();
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        mana = maxMana;
        audioManager = AudioManager.Instance;
        audioManager.PlaySound("MonsterRoar");
    }

    // Update is called once per frame
    void Update()
    {
        //Move();
        agent.speed = speed;
        //Vector3 lookVector = player.transform.position - transform.position;
        //lookVector.y = transform.position.y;
        //Quaternion rot = Quaternion.LookRotation(lookVector);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);

        transform.LookAt(player, Vector3.up);

        if (rb.velocity.magnitude > 0f)
        {
            anim.SetBool("isMoving", true);
        }
        else
            anim.SetBool("isMoving", false);
        if (mana > maxMana)
        {
            mana = maxMana;
        }

        if (cdManaReg_timer <= 0f)
        {
            mana += manaReg_amount;
            cdManaReg_timer = cdManaReg;
        }

        healthSlider.value = currentHealth / maxHealth;
        manaSlider.value = mana / maxMana;

        cdManaReg_timer -= Time.deltaTime;

        tree.GetVariable("Mana").SetValue(mana); 
        tree.GetVariable("Health").SetValue(currentHealth);
    }

    public void GetDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0f)
        {
            gameManager.score += gainScore;
            gameManager.kill++;
            //Character died.
            gameManager.OnWin.Invoke(1f);
        }
    }

    public void GetHeal(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;
        //Cant be overhealed
    }

}