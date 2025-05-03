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

    private Vector2 throwPos = new Vector2(-3.3f, 3f);
    private Rigidbody2D barrelRB;
    private SpriteRenderer sRend;
    public bool canThrow = true;
    public float minThrowTime = 3.0f;

    // Sound effect variables
    public List<AudioClip> kongSounds = new List<AudioClip>();
    private AudioSource source;
    private bool isPlaying = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        sRend=GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        PrepareThrow();

    }

    void Update()
    {

        
        switch (kState)
        {
            case KongState.Idle:
                anim.Play(stateName: "KongIdle");
                //playKongSound(0);
                if (canThrow)
                {
                    canThrow = false;
                    float randomDelay = Random.Range(0f, 2.0f);
                    Invoke("PrepareThrow", minThrowTime+randomDelay);
                }

                break;
                
                case KongState.BarrelThrow:
                anim.Play(stateName: "KongThrow");
                //playKongSound(1);

                break;
            
            
            

        }
    }
    void PrepareThrow()
    {
        kState = KongState.BarrelThrow;
        Invoke("ThrowBarrel", 1f);
    }
    void ThrowBarrel()
    {
        //Debug.Log("Kong has thrown a barrel");
        anim.Play(stateName: "KongThrow");
        anim.SetTrigger("Throw");
        barrel = Instantiate(barrelPrefab) as GameObject;
        barrel.transform.position = throwPos;
        barrelRB = barrel.GetComponent<Rigidbody2D>(); 
        barrelRB.velocity =new Vector2(2, 0);
        kState = KongState.Idle;   
        canThrow = true;

    }

    public void playKongSound(int index) {
        /*
            0 : idle
            1 : throw
            2 : reaction to death
        */

        AudioClip clip = kongSounds[index];

        // For testing before recording
        if (clip == null)
        {
            Debug.Log("AudioClip not found at index: " + index);
            return;
        }
        else if (isPlaying) {
            return;
        }

        isPlaying = true;

        source.PlayOneShot(clip);
        StartCoroutine(Wait(clip.length));

        isPlaying = false;
    }

    IEnumerator Wait(float waitTime) {
        yield return new WaitForSecondsRealtime(waitTime);
    }
}