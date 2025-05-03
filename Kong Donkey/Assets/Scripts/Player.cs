using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState {Idle, Jump, Run, Dying, Hammer, Ladder}

public class Player : MonoBehaviour
{
    
    [Header("Inscribed")]
    public float speed = 2;

    // Sound effect variables
    public List<AudioClip> playerSounds = new List<AudioClip>();
    private AudioSource source;
    public float idleSoundChance = 0.05f;
    public Iggy iggy;

    [Header("Dynamic")]
    public int dirHeld = -1;
    public bool canClimb = false;

    public PlayerState state = PlayerState.Idle;
    private bool isPlaying = false;

    private Rigidbody2D rigid;
    private Animator anim; 
    private SpriteRenderer sRend;
    private Vector2[] directions = new Vector2[2]
    {
        Vector2.right, Vector2.left
    };

    private KeyCode[] keys = new KeyCode[]
    {
        KeyCode.RightArrow, KeyCode.LeftArrow, 
        KeyCode.D, KeyCode.A
    };

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sRend = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        dirHeld = -1;
        //Check wich direction is currently being held
        for (int i= 0;i<keys.Length; i++)
        {
            if (Input.GetKey(keys[i]))
                dirHeld = i % 2;
        }
    
        Vector2 vel = Vector2.zero;
        if (state != PlayerState.Ladder)
        {
            //If a direction is held: update velocity
            if(dirHeld > -1)
            {
                vel = directions[dirHeld];
                if (state != PlayerState.Jump)
                    state = PlayerState.Run;
                if (dirHeld == 0)
                    sRend.flipX = false;
                else if (dirHeld == 1)
                    sRend.flipX = true;
            }   
            else//No direction is held
            {
                state = PlayerState.Idle;
            }
        }    
        //Check if the player can move up or down  ladder
        if(canClimb || state == PlayerState.Ladder)
        {
                //Check if the player wants to climb the ladder
                if(Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.W))
                {
                    state = PlayerState.Ladder;
                    vel = Vector2.up;
                    gameObject.layer = LayerMask.NameToLayer("Ladder");

                }
                else if (Input.GetKey(KeyCode.DownArrow)||Input.GetKey(KeyCode.S))
                {
                    state = PlayerState.Ladder;
                    vel = Vector2.down;
                    gameObject.layer = LayerMask.NameToLayer("Ladder"); 
                }
                //If the player tries to move left or right while at the end of a ladder
                if (dirHeld>-1 && canClimb)
                {
                    state = PlayerState.Run;
                    vel = directions[dirHeld];
                    gameObject.layer = LayerMask.NameToLayer("Default"); 
   
                }

        }
        rigid.velocity = vel * speed;
        if (rigid.velocity.y ==0 && state==PlayerState.Jump)
            state =PlayerState.Idle;

        //If the player pressed jump add vertical velocity
        if (Input.GetKey(KeyCode.Space) && (state != PlayerState.Jump  && state !=PlayerState.Ladder))
        {
            Jump();
        }

        //Decide which animation to play
        switch (state)
        {
            case PlayerState.Run:
                anim.Play(stateName: "PlayerRun");
                //playPlayerSound(0);
                rigid.gravityScale = 5;

                break;
            case PlayerState.Idle:
                anim.Play(stateName: "PlayerIdle");
                rigid.gravityScale = 5;

                if (Random.value < idleSoundChance)
                {
                    //playPlayerSound(1);
                }

                break;
            case PlayerState.Jump:
                anim.Play(stateName: "PlayerJump");
                //playPlayerSound(2);
                rigid.gravityScale = 5;

                break;
            case PlayerState.Ladder:
                anim.Play(stateName: "PlayerClimb");
                //iggy.playIggySound(2);
                rigid.gravityScale = 0;

                break;

            case PlayerState.Dying:
                anim.Play(stateName: "PlayerDeath");
                // playPlayerSound(3);
                rigid.velocity = Vector2.zero;
                rigid.gravityScale = 0;
                break;
        }
        
    }

    void Jump()
    {
        state= PlayerState.Jump;
        Vector2 vel = rigid.velocity;
        vel.y +=4;
        rigid.velocity =vel;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Player collided with something");
        if(other.CompareTag("Ladder"))
        {
            Debug.Log("Player touched a ladder");
            canClimb = true;
        }
        // Goal trigger
        if(other.gameObject.CompareTag("Goal"))
        {
            Debug.Log("Player reached the goal!");
            // Play victory sounds here

            // Play victory animation here

            // Save lifes for score and move to victory screen
            FindObjectOfType<GameManager>().Victory();

            

        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("Player collided with something");
        if(other.CompareTag("Ladder"))
        {
            Debug.Log("Player left a ladder");
            canClimb =false;
        }
    }

    public void playPlayerSound(int index) {
        /*
            0 : run
            1 : idle
            2 : jump
            3 : death
            4 : respawn
        */

        AudioClip clip = playerSounds[index];

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

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Barrel"))
        {
            Debug.Log("Player collided with a barrel");
            //playPlayerSound(3);
            state = PlayerState.Dying;
            OnDeath();
        }
    }
    private void OnDeath(){
        Debug.Log("Player died!");
        this.gameObject.SetActive(false); // Instead of this, use death animation here, and transfer marquio to "invincible" layer
        destroyBarrel();
        FindObjectOfType<GameManager>().playerDied();
    }

    private void destroyBarrel(){
        GameObject[] barrelArray = GameObject.FindGameObjectsWithTag("Barrel");
        foreach(GameObject barrel in barrelArray)
        {
            Destroy(barrel);
        }
    }

}
