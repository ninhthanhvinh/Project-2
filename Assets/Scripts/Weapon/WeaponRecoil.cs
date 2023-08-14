using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [SerializeField] CinemachineImpulseSource impulseSource;
    [SerializeField] float impulseForce = 1f;
    [SerializeField] float impulseTime = 0.1f;

    public void GenerateImpulse()
    {
        impulseSource.GenerateImpulseWithVelocity(Vector3.forward * impulseForce);
    }
}
