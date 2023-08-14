using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponReload : MonoBehaviour
{
    public Animator rigController;
    public WeaponAnimationEvents animationEvents;
    private Animator Animator;

    private PlayerInput playerInput;
    private InputAction reloadAction;

    public ActiveWeapon weapon;
    public Transform leftHand;
    GameObject magazineHand;

    private AudioManager audioManager;
    // Start is called before the first frame update
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        reloadAction = playerInput.actions["Reload"];
        animationEvents.WeaponAnimationEvent.AddListener(OnAnimationEvent);
        Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        audioManager = AudioManager.Instance;
    }

    private void OnEnable()
    {
        reloadAction.performed += _ => Reload();
    }

    private void OnDisable()
    {
        reloadAction.performed -= _ => Reload();
    }

    // Update is called once per frame
    public void Reload()
    {
        rigController.SetTrigger("reload");
        audioManager.PlaySound("Reload");
    }

    public void OnAnimationEvent(string eventName)
    {
        switch (eventName)
        {
            case "detach_magazine":
                {
                    RaycastShoot weapon1 = weapon.weapon;
                    magazineHand = Instantiate(weapon1.magazine, leftHand, true);
                    weapon1.magazine.SetActive(false);
                    break;
                }
            case "drop_magazine":
                {
                    GameObject dropMagazine = Instantiate(magazineHand, magazineHand.transform.position, magazineHand.transform.rotation);
                    dropMagazine.AddComponent<Rigidbody>();
                    dropMagazine.AddComponent<BoxCollider>();
                    magazineHand.SetActive(false) ;
                    break;
                }
            case "fill_magazine":
                {
                    magazineHand.SetActive(true);
                    break;
                }
            case "attach_magazine":
                {
                    RaycastShoot weapon1 = weapon.weapon;
                    weapon1.magazine.SetActive(true) ;
                    weapon1.ammoCount = weapon1.loadSize;
                    Destroy(magazineHand);
                    break;
                }
            default:
                break;
        }
    }
}
