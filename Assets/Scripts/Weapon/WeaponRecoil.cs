using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [HideInInspector]
    public Cinemachine.CinemachineVirtualCamera playerCamera;

    public float verticalRecoil;

    public void GenerateRecoils()
    {
        //playerCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>().m_ -= verticalRecoil;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
