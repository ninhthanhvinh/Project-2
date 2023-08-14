using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveWeapon : MonoBehaviour
{
    public Transform crosshair;
    public Transform target;
    public UnityEngine.Animations.Rigging.Rig handIK;
    public Transform weaponParent;

    public Transform weaponLeftGrip;
    public Transform weaponRightGrip;

    public TextMeshProUGUI ammoCount;

    private PlayerInput playerInput;
    private InputAction shootAction;
    private InputAction pauseAction;

    private GameManager GameManager;

    private bool isFiring = false;
    private float accumulatedTime;
    private float interval;
    private WeaponReload weaponReload;

    public RaycastShoot weapon;
    public Animator rigController;
    public WeaponRecoil weaponRecoil;

    private AudioManager audioManager;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions["Shoot"];
        pauseAction = playerInput.actions["PauseGame"];
        weaponReload = GetComponent<WeaponReload>();
        GameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void Start()
    {
        RaycastShoot existingWeapon = GetComponentInChildren<RaycastShoot>();
        if (existingWeapon != null)
        {
            Equip(existingWeapon);
        }

        audioManager = AudioManager.Instance;
    }

    private void OnEnable()
    {
        shootAction.performed += _ => StartFiring();
        shootAction.canceled += _ => StopFiring();
        pauseAction.performed += _ => OnPause();
    }

    private void OnDisable()
    {
        shootAction.performed -= _ => StartFiring();
        shootAction.canceled -= _ => StopFiring();
        pauseAction.performed -= _ => OnPause();
    }

    private void StartFiring()
    {
        isFiring = true;
    }

    private void StopFiring()
    {
        isFiring = false;
    }

    private void OnPause()
    {
        GameManager.Pause();
        Debug.Log("Pause");
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon)
        {
            if (isFiring && weapon.ammoCount > 0)
            {
                while (accumulatedTime <= 0f)
                {
                    weapon.FireShoot();
                    weaponRecoil.GenerateImpulse();
                    accumulatedTime = interval;
                    audioManager.PlaySound("RifleShot");
                }
                accumulatedTime -= Time.deltaTime;
            }
            if (weapon.ammoCount <= 0)
            {
                weaponReload.Reload();

            }
            ammoCount.SetText(weapon.ammoCount.ToString());        
        }
    }

    public void Equip(RaycastShoot newWeapon)
    {
        if (weapon)
            Destroy(weapon.gameObject);
        weapon = newWeapon;
        interval = 1 / weapon.fireRate;
        weapon.crosshair = crosshair;
        weapon.target = target;
        weapon.transform.parent = weaponParent;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        rigController.Play("equip_" + weapon.weaponName);
        Debug.Log(rigController.GetCurrentAnimatorClipInfo(0).ToString());
        Invoke(nameof(SetAnimationDelayed), 0.001f);
    }

    private void SetAnimationDelayed()
    {
    }

}
