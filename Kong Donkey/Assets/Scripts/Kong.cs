using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KongState{Idle, BarrelThrow}

public class Kong : MonoBehaviour
{

    public KongState kState = KongState.Idle;
    private Animator anim;
    public GameObject barrelPrefab;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        
        switch (kState)
        {
            case KongState.BarrelThrow:
                anim.Play(stateName: "KongThrow");
                break;
            case KongState.Idle:
                anim.Play(stateName: "KongIdle");
                break;
            
            

        }
    }

    void ThrowBarrel()
    {
        kState = KongState.BarrelThrow;

    }

}
