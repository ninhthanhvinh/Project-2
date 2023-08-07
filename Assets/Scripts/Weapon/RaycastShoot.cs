using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastShoot : MonoBehaviour
{
    public string weaponName;
    [SerializeField]
    private ParticleSystem[] muzzleFlashs;
    [SerializeField]
    private float dmgPerBullet;
    [SerializeField]
    private ParticleSystem hitEffect;
    [SerializeField]
    private TrailRenderer tracerEffect;
    public Transform target;
    [SerializeField]
    private Transform raycastOrigin;
    public Transform crosshair;
    public float fireRate;
    public int loadSize = 30;

    public int ammoCount;


    public GameObject magazine;

    private Ray ray;
    private RaycastHit hitInfo;
    // Start is called before the first frame update
    private void Awake()
    {
        ammoCount = loadSize;
    }
    public void FireShoot()
    {
        if (ammoCount <= 0)
        {
            return;
        }
        foreach (var muzzleFlash in muzzleFlashs)
        {
            muzzleFlash.Emit(1);
        }

        ray.origin = raycastOrigin.position;

        var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);
        tracer.AddPosition(ray.origin);
        ammoCount--;
        if (target.position != Vector3.positiveInfinity)
        {
            ray.direction = target.position - raycastOrigin.position;


            if (Physics.Raycast(ray, out hitInfo))
            {
                hitEffect.transform.position = hitInfo.point;
                hitEffect.transform.forward = hitInfo.normal;
                hitEffect.Emit(1);

                tracer.transform.position = hitInfo.point;

                if (hitInfo.collider.GetComponent<IDamageable>() != null)
                {
                    hitInfo.collider.GetComponent<IDamageable>().GetDamage(dmgPerBullet);
                }
            }

            else tracer.transform.position = crosshair.position;
        }
    }
}
