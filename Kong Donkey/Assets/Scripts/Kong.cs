using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KongState{Idle, BarrelThrow}

public class Kong : MonoBehaviour
{

    public KongState kState = KongState.Idle;
    private Animator anim;
    
    //For barrel throwing
    public GameObject barrelPrefab;
    private GameObject barrel;
    private GameObject barrelSpawn;

    private Vector2 throwPos;
    private Rigidbody2D barrelRB;
    private SpriteRenderer sRend;

    // Sound effect variables
    public List<AudioClip> kongSounds = new List<AudioClip>();
    private AudioSource source;

    void Awake()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        sRend=GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Transform launchPointTrans = transform.Find("barrelSpawn");
        barrelSpawn = launchPointTrans.gameObject;
        throwPos = launchPointTrans.position;
        ThrowBarrel();

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
    void PrepareThrow()
    {
        kState = KongState.BarrelThrow;
        Invoke("ThrowBarrel", 2);
    }
    void ThrowBarrel()
    {
        Debug.Log("Kong has thrown a barrel");
        anim.Play(stateName: "KongThrow");
        anim.SetTrigger("Throw");
        barrel = Instantiate(barrelPrefab) as GameObject;
        barrel.transform.position = throwPos;
        barrelRB = barrel.GetComponent<Rigidbody2D>(); 
        barrelRB.velocity =new Vector2(2, 0);
        
        kState = KongState.Idle;   
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