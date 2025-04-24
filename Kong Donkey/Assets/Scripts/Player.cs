using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Inscribed")]
    public float speed = 5;

    [Header("Dynamic")]
    public int dirHeld = -1;

    private Rigidbody2D rigid;
    private Animator anim; 
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
            vel = directions[dirHeld];

        //If the player pressed jump add vertical velocity
        if (Input.GetKey(KeyCode.Space))
        {
            vel.y += 10;
        }
        
        rigid.velocity = vel * speed;
        
    }


}
