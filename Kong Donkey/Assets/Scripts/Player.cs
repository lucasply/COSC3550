using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {Idle, Jump, Run, Hammer, Ladder}

public class Player : MonoBehaviour
{
    
    [Header("Inscribed")]
    public float speed = 2;

    [Header("Dynamic")]
    public int dirHeld = -1;

    public PlayerState state = PlayerState.Idle;

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

        rigid.velocity = vel * speed;
        if (rigid.velocity.y ==0 && state==PlayerState.Jump)
            state =PlayerState.Idle;

        //If the player pressed jump add vertical velocity
        if (Input.GetKey(KeyCode.Space) && state != PlayerState.Jump)
        {
            Jump();
        }

        //Decide which animation to play
        switch (state)
        {
            case PlayerState.Run:
                anim.Play(stateName: "PlayerRun");
                break;
            case PlayerState.Idle:
                anim.Play(stateName: "PlayerIdle");
                break;
            case PlayerState.Jump:
                anim.Play(stateName: "PlayerJump");
                break;
            case PlayerState.Ladder:
                anim.Play(stateName: "PlayerClimb");
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

    void OnCollisionEnter2D(Collision2D coll)
    {
        //Debug.Log("Player collided with something");
    }

}
