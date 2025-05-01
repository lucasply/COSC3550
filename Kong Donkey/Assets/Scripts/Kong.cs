using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KongState{Idle, BarrelThrow}

public class Kong : MonoBehaviour
{

    public KongState kState = KongState.Idle;
    private Animator anim;
    public GameObject barrelPrefab;

    // Sound effect variables
    public List<AudioClip> kongSounds = new List<AudioClip>();
    private AudioSource source;

    void Awake()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {

        
        switch (kState)
        {
            case KongState.BarrelThrow:
                anim.Play(stateName: "KongThrow");
                playKongSound(1);

                break;
            case KongState.Idle:
                anim.Play(stateName: "KongIdle");
                playKongSound(0);

                break;
            
            

        }
    }

    void ThrowBarrel()
    {
        kState = KongState.BarrelThrow;
    }

    public void playKongSound(int index) {
        /*
            0 : idel
            1 : throw
        */

        AudioClip clip = kongSounds[index];

        // For testing before recording
        if (clip == null)
        {
            Debug.LogError("AudioClip not found at index: " + index);
            return;
        }

        source.PlayOneShot(clip);
    }

}